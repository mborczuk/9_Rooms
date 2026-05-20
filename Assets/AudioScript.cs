using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private static AudioScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static void PlayAudio()
    {
        instance.GetComponent<AudioSource>().Play();
    }
    public static void StopAudio()
    {
        instance.GetComponent<AudioSource>().Stop();
    }
    public static bool IsPlaying()
    {
        return instance.GetComponent<AudioSource>().isPlaying;
    }
}