using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PLayerSoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip jump, damage, death;

    public void JumpSound(){
        src.clip = jump;
        src.Play();
    }

    public void DamageSound(){
        src.clip = damage;
        src.Play();  
    }

    public void DeathSound(){
        src.clip = death;
        src.Play();   
    }
}
