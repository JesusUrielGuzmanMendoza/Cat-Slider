using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static AudioClip JumpSound, EatBreadSound, DrinkSound, RocketShipSound;
    static AudioSource AudioSource;
    public AudioClip AudioClip;

    // Start is called before the first frame update
    void Start() {
        JumpSound = Resources.Load<AudioClip>("JumpSound");    
        EatBreadSound = Resources.Load<AudioClip>("EatBreadSound");    
        DrinkSound = Resources.Load<AudioClip>("DrinkSound");    
        RocketShipSound = Resources.Load<AudioClip>("RocketShipSound");    
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(AudioClip);
        AudioSource.PlayScheduled(AudioSettings.dspTime + AudioClip.length);
    }

    public static void PlaySound(string Sound) {
        switch (Sound) {
            case "JumpSound":
                AudioSource.PlayOneShot(JumpSound);
                break;
            case "EatBreadSound":
                AudioSource.PlayOneShot(EatBreadSound);
                break;
            case "DrinkSound":
                AudioSource.PlayOneShot(DrinkSound, 3f);
                break;
            case "RocketShipSound":
                AudioSource.PlayOneShot(RocketShipSound);
                break;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
