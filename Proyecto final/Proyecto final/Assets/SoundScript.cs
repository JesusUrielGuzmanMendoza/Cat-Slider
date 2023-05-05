using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static AudioClip JumpSound, EatBreadSound;
    static AudioSource AudioSource;

    // Start is called before the first frame update
    void Start() {
        JumpSound = Resources.Load<AudioClip>("JumpSound");    
        EatBreadSound = Resources.Load<AudioClip>("EatBreadSound");    
        AudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string Sound) {
        switch (Sound) {
            case "JumpSound":
                AudioSource.PlayOneShot(JumpSound);
                break;
            case "EatBreadSound":
                AudioSource.PlayOneShot(EatBreadSound);
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
