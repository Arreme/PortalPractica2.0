using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Audios
{
    BUTTON,
    TELEPORT,
    LASER1,
    LASER2,
    OBJBREAK,
    ACIDDEAD,
    BURNT
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    [SerializeField] private AudioSource _audioPlayer;

    [SerializeField] private AudioClip[] _audioList;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public void PlayAudio(int audio)
    {
        _audioPlayer.clip = _audioList[audio];
        _audioPlayer.Play();
    }
}
