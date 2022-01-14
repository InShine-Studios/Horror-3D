using UnityEngine.Audio;
using UnityEngine;
using System;

/*
 * Class to manage audio with array of Sound objects.
 * Only used for BGM and SFX that is globally used.
 */
public class AudioManager : MonoBehaviour
{

    #region Variables
    [Tooltip("List of Playable Sounds")]
    public Sound[] sounds;
    #endregion

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
