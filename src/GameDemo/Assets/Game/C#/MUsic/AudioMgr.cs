using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public static AudioMgr Instance;
    public delegate void AudioSourceDelegate(AudioSource audioSurce);
    /// <summary>
    /// 带淡入效果的播放
    /// </summary>
    public static AudioSourceDelegate OnPlayMusicEvent;
    /// <summary>
    /// 带淡出效果的停止
    /// </summary>
    public static AudioSourceDelegate OnStopMusicEvent;
    public delegate void BackGroundAudioSourceDelegate(float AudioVolume);
    public static BackGroundAudioSourceDelegate OnLowerBackGroundMusicEvent;
    public static BackGroundAudioSourceDelegate OnIncreaseBackGroundMusicEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        OnPlayMusicEvent += PlayMusic;
        OnStopMusicEvent += StopMusic;

        OnLowerBackGroundMusicEvent += LowerBGMusic;
        OnIncreaseBackGroundMusicEvent += IncreaseBGMusic;
    }

   public void DeleteAudio()
    {
        OnPlayMusicEvent -= PlayMusic;
        OnStopMusicEvent -= StopMusic;

        OnLowerBackGroundMusicEvent -= LowerBGMusic;
        OnIncreaseBackGroundMusicEvent -= IncreaseBGMusic;
        Destroy(gameObject);
    }

    void PlayMusic(AudioSource audioSurce)
    {
      
       StartCoroutine(IePlayMusic(audioSurce));
      
    }

    void StopMusic(AudioSource audioSurce)
    {
        StartCoroutine(IeStopMusic(audioSurce));
   
    }

    IEnumerator IePlayMusic(AudioSource audioSurce)
    {
        //声音渐变变大
        while (audioSurce.volume < 1)
        {
            audioSurce.volume += 0.1f;
            yield return new WaitForSeconds(0.07f);
        }
        audioSurce.Play();
    }


    IEnumerator IeStopMusic(AudioSource audioSurce)
    {
        //声音渐变变小
        while (audioSurce.volume > 0)
        {
            audioSurce.volume -= 0.1f;
            yield return new WaitForSeconds(0.07f);
        }
        audioSurce.Stop();
    }

    Coroutine LBGMusic=null;
    Coroutine IBGMusic = null;
    void LowerBGMusic(float AudioVolume = 1)
    {
       
        LBGMusic =StartCoroutine(LowerStopMusic(GetComponent<AudioSource>(), AudioVolume));

        if (IBGMusic != null) {
            StopCoroutine(IBGMusic);
        IBGMusic = null;
        }
    }

    void IncreaseBGMusic(float AudioVolume = 0)
    {
      
            IBGMusic = StartCoroutine(IncreaseMusic(GetComponent<AudioSource>(), AudioVolume));
        if (LBGMusic != null)
        {
            StopCoroutine(LBGMusic);
            LBGMusic = null;
        }
    }

    IEnumerator IncreaseMusic(AudioSource audioSurce, float AudioVolume)
    {

        //声音渐变变大
        while (audioSurce.volume < AudioVolume)
        {
            audioSurce.volume += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        audioSurce.volume = AudioVolume;
    }


    IEnumerator LowerStopMusic(AudioSource audioSurce, float AudioVolume)
    {
        //声音渐变变小
        while (audioSurce.volume > AudioVolume)
        {
            audioSurce.volume -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }

        audioSurce.volume = AudioVolume;
    }
}
