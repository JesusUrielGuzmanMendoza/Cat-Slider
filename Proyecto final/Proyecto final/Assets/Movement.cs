

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4, None = 5 };
public enum Gamemodes { Cube = 0, Ship = 1 };

public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public Gamemodes CurrentGamemode;
    //                       0      1      2       3      4
    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f, 0.0f };

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    public Transform Sprite;

    Rigidbody2D rb;

    int Gravity = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    IEnumerator GainSpeed(Speeds Speed)
    {
        Speeds PreviousSpeed = CurrentSpeed;
        CurrentSpeed = Speed;
        yield return new WaitForSeconds(5f);
        CurrentSpeed = PreviousSpeed;

    }
    void FixedUpdate()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (rb.velocity.y * Gravity < -24.2f)
            rb.velocity = new Vector2(rb.velocity.x, -24.2f * Gravity);

        if (rb.velocity.y * Gravity > 24.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 24.2f * Gravity);
        }
        Invoke(CurrentGamemode.ToString(), 0);
    }

    bool OnGround()
    {
        return Physics2D.OverlapBox(GroundCheckTransform.position + Vector3.up - Vector3.up * (Gravity - 1 / -2), Vector2.right * 1.1f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

    bool TouchingWall()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.55f), Vector2.up * 0.8f + (Vector2.right * GroundCheckRadius), 0, GroundMask);
    }

    void Cube()
    {
        if (TouchingWall())
        {
            print("Touched a wall");
            CurrentSpeed = Speeds.None;
            rb.velocity = Vector3.zero;
        }

        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 360) * 360;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButton(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f * Gravity, ForceMode2D.Impulse);
            }
        }
        else
        {
            // Un-comment if you want rotation when the player jumps (doesn't work very well)
            Sprite.Rotate(Vector3.back, 2 * 452.4152186f * Time.deltaTime * Gravity);
        }

        rb.gravityScale = 12.41067f * Gravity;
    }

    void Ship()
    {
        Sprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);

        if (Input.GetMouseButton(0))
            rb.gravityScale = -4.314969f;
        else
            rb.gravityScale = 4.314969f;

        rb.gravityScale = rb.gravityScale * Gravity;
    }

    public void ChangeThroughPortal(Gamemodes Gamemode, Speeds Speed, int State)
    {
        switch (State)
        {
            case 0:
                StopCoroutine(GainSpeed(Speed));
                StartCoroutine(GainSpeed(Speed));
                ScoreManager.instance.AddPoint();
                break;
            case 1:
                CurrentGamemode = Gamemode;
                break;
            case 2:
                Gravity *= -1;
                rb.gravityScale = Mathf.Abs(rb.gravityScale) * Gravity;
                break;
        }
    }
}