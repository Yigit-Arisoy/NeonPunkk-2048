using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] AudioClip[] musicList;
    [SerializeField] public AudioClip mergeClip;
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource fxSource;
    [SerializeField] GameObject soundFxSprite, musicSprite;
    [SerializeField] bool isSoundFxOn = true;
    [SerializeField] Sprite musicOnSprite, musicOffSprite, soundFxOnSprite, soundFxOffSprite;

    static  AudioManager instance;

    private void Start()
    {
        if (!musicSource.isPlaying)
            ShufflePlay();
    }
    void ShufflePlay()
    {
        int songIndex = Random.Range(0, musicList.Length);
        musicSource.clip = musicList[songIndex];
        musicSource.Play();
    }

    public void startStopMusic()
    {
        if(musicSource.isPlaying)
        {
            musicSource.Pause();
            musicSprite.GetComponent<SpriteRenderer>().sprite = musicOffSprite;
        }

        else
        {
            musicSource.Play();
            musicSprite.GetComponent<SpriteRenderer>().sprite = musicOnSprite;
        }
    }

    public void startStopFxSounds()
    {
        if (isSoundFxOn)
        {
            isSoundFxOn = false;
            soundFxSprite.GetComponent<SpriteRenderer>().sprite = soundFxOffSprite;
        }
        else
        {
            isSoundFxOn = true;
            soundFxSprite.GetComponent<SpriteRenderer>().sprite = soundFxOnSprite;
        }
    }

    public void playSoundFx(AudioClip clip)
    {
        if(isSoundFxOn)
        {
            fxSource.clip = clip;
            fxSource.Play();
        }
    }

}
