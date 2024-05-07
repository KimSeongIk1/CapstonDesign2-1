using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMange : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip[] audioClip;
    public void AttackSound(int index)
    {
        audioSources[index].Play();
        //audioSources[index].PlayOneShot(audioNum);
    }
}
