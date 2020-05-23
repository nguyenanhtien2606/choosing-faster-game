using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip mainAudio;
    [SerializeField] AudioClip correntAudio;
    [SerializeField] AudioClip wrongAudio;

    public AudioClip CorrentAudio
    {
        get { return correntAudio; }
        set { correntAudio = value; }
    }

    public AudioClip WrongAudio
    {
        get { return wrongAudio; }
        set { wrongAudio = value; }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = mainAudio;
        audioSource.Play();
    }

    public void PlayConrrentAudio()
    {
        audioSource.PlayOneShot(correntAudio);
    }

    public void PlayWrongAudio()
    {
        audioSource.PlayOneShot(wrongAudio);
    }
}
