using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSoundManager : MonoBehaviour
{
    public static ScSoundManager gSoundManager;

    public AudioSource[] audiosBgms;
    public AudioSource[] audiosEffects;

    private void Awake()
    {
        gSoundManager = this;
    }

    public void InitSounds()
    {
        for(int i = 0; i<audiosBgms.Length; i++)
        {
            audiosBgms[i].Stop();
        }

        for(int i = 0; i<audiosEffects.Length; i++)
        {
            audiosEffects[i].Stop();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSounds();

        audiosBgms[0].Play();
    }

}
