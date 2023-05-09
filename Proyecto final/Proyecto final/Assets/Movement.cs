using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels {
  One = 0,
  Two,
  Three
}

public enum Gamemodes { Cube = 0, Ship }

public class Movement : MonoBehaviour {
  public float CurrentSpeed;
  private float previousSpeed;
  private float previousDrag;
  public Gamemodes CurrentGamemode;
  int[] PointValues = { 100, 500, 1000, 5000 };
  Levels CurrentLevel = Levels.One;
  public Transform GroundCheckTransform;
  public float GroundCheckRadius;
  public LayerMask GroundMask;
  public Transform Sprite;
  Rigidbody2D rb;
  int Gravity = 1;
  bool Jumping = false;
  private bool switchingGravity = false;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  IEnumerator GainSpeed() {
    SoundScript.PlaySound("DrinkSound");
    CurrentSpeed *= 1.2f;
    yield return new WaitForSeconds(3f);
    CurrentSpeed /= 1.2f;
  }

  void ToggleGravity() {
    previousDrag = rb.drag;
    rb.drag = 10f;
    switchingGravity = true;
    Gravity *= -1;
    rb.gravityScale = Mathf.Abs(rb.gravityScale) * Gravity;
    previousSpeed = CurrentSpeed;
    CurrentSpeed = 0f;
  }

  IEnumerator EnableBalloons() {
    SoundScript.PlaySound("MagicSound");
    SpriteScript.instance.EnableGravitySprite();
    ToggleGravity();
    yield return new WaitForSeconds(8f);
    SpriteScript.instance.EnablePlayerSprite();
    ToggleGravity();
  }

  IEnumerator EnableShip() {
    SoundScript.PlaySound("RocketShipSound");
    SpriteScript.instance.EnableShipSprite();
    CurrentGamemode = Gamemodes.Ship;
    yield return new WaitForSeconds(6f);
    CurrentGamemode = Gamemodes.Cube;
    SpriteScript.instance.EnablePlayerSprite();
  }

  void FixedUpdate() {
    transform.position +=
        Vector3.right * CurrentSpeed * Time.deltaTime;

    if (rb.velocity.y * Gravity < -24.2f)
      rb.velocity = new Vector2(rb.velocity.x, -24.2f * Gravity);

    if (rb.velocity.y * Gravity > 24.2f) {
      rb.velocity = new Vector2(rb.velocity.x, 24.2f * Gravity);
    }

    Invoke(CurrentGamemode.ToString(), 0);
  }

  bool OnGround() {
    return Physics2D.OverlapBox(
        GroundCheckTransform.position + Vector3.up -
            Vector3.up * (Gravity - 1 / -2),
        Vector2.right * 1.1f + Vector2.up * GroundCheckRadius,
        0,
        GroundMask);
  }

  bool TouchingWall() {
    return Physics2D.OverlapBox(
        (Vector2)transform.position + (Vector2.right * 0.55f),
        Vector2.up * 0.8f + (Vector2.right * GroundCheckRadius),
        0,
        GroundMask);
  }

  void Cube() {
    if (TouchingWall()) {
      SceneManager.LoadScene((int)CurrentLevel);
    }

    if (OnGround()) {
      Jumping = false;
      Vector3 Rotation = Sprite.rotation.eulerAngles;
      Rotation.z = Mathf.Round(Rotation.z / 360) * 360;
      Sprite.rotation = Quaternion.Euler(Rotation);

      if (switchingGravity) {
        CurrentSpeed = previousSpeed;
        rb.drag = previousDrag;
        switchingGravity = false;
      }

      if (Input.GetMouseButton(0)) {
        SoundScript.PlaySound("JumpSound");
        Jumping = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 26.6581f * Gravity, ForceMode2D.Impulse);
      }
    } else if (Jumping) {
      Sprite.Rotate(Vector3.back, 2 * 452.4152186f * Time.deltaTime * Gravity);
    }

    rb.gravityScale = 12.41067f * Gravity;
  }

  void Ship() { 
    if (TouchingWall()) {
      SceneManager.LoadScene((int)CurrentLevel);
    }

    Sprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);

    if (Input.GetMouseButton(0))
      rb.gravityScale = -4.314969f;
    else
      rb.gravityScale = 4.314969f;

    rb.gravityScale = rb.gravityScale * Gravity;
  }

  void ChangeLevel(Levels Level) {
    CurrentLevel = Level;
    SceneManager.LoadScene((int)Level);
  }

  public void ChangeThroughPortal(Gamemodes Gamemode, Levels Level, int State) {
    switch (State) {
      case 0:
        SoundScript.PlaySound("EatBreadSound");
        ScoreManager.instance.UpdateScore(PointValues[State]);
        break;
      case 1:
        StopCoroutine(GainSpeed());
        StartCoroutine(GainSpeed());
        ScoreManager.instance.UpdateScore(PointValues[State]);
        break;
      case 2:
        StopCoroutine(EnableBalloons());
        StartCoroutine(EnableBalloons());
        ScoreManager.instance.UpdateScore(PointValues[State]);
        break;
      case 3:
        StopCoroutine(EnableShip());
        StartCoroutine(EnableShip());
        ScoreManager.instance.UpdateScore(PointValues[State]);
        break;
      case 4:
        ChangeLevel(Level);
        break;
    }
  }
}
