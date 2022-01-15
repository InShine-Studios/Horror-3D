using UnityEngine.Audio;
using UnityEngine;
using System;

/*
 * Class to manage audio with array of Sound objects.
 * Only used for BGM and SFX that is globally used.
 */
public class AudioManager : MonoBehaviour
{
    public void Play(string name)
    {
        GameObject s = GameObject.Find(name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.GetComponent<AudioSource>().Play();
    }
}