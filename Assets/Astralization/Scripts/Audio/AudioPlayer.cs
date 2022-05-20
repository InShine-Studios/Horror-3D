using System.Collections.Generic;
using UnityEngine;

/*
 * Class to manage audio with array of Sound objects.
 * Only used for BGM and SFX that is globally used.
 */
public class AudioPlayer : MonoBehaviour
{
    #region Variables
    private Dictionary<string, AudioSource> _audioMap;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _audioMap = new Dictionary<string, AudioSource>();
        for (int i = 0; i < transform.childCount; i++) 
        {
            Transform child = transform.GetChild(i);
            _audioMap.Add(child.name, child.GetComponent<AudioSource>());
        }
    }
    #endregion

    #region AudioPlayer
    public void Play(string name)
    {
        _audioMap[name].Play();
    }
    #endregion
}
