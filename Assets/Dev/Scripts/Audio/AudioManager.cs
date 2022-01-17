using UnityEngine;

/*
 * Class to manage audio with array of Sound objects.
 * Only used for BGM and SFX that is globally used.
 */
public class AudioManager : MonoBehaviour
{
    public void Play(string name)
    {
        Transform sound = transform.Find(name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sound.GetComponent<AudioSource>().Play();
    }
}