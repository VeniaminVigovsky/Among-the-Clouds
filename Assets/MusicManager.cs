using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioClip introClip, loopClip;

    AudioSource audioSource;
    double introDuration;
    //Coroutine coroutine;

    //private static MusicManager instance = null;
    //public static MusicManager Instance { get { return instance; } }

    //private void Awake()
    //{
    //    if  (instance != null && instance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    else
    //    {
    //        instance = this;
    //    }

    //    DontDestroyOnLoad(this.gameObject);
    //}

    public delegate void PlayIntroLoopHandler();

    public event PlayIntroLoopHandler PlayIntroLoopStarted;
    private void OnEnable()
    {
        PlayIntroLoopStarted += PlayIntro;
        PlayIntroLoopStarted += PlayLoopScheduled;
    }

    private void OnDestroy()
    {
        PlayIntroLoopStarted -= PlayIntro;
        PlayIntroLoopStarted -= PlayLoopScheduled;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //if (audioSource != null)
        //audioSource.volume = 0;
        //if (coroutine == null)
        //{
        //    coroutine = StartCoroutine(VolumeUp());

        //}

        if (introClip != null)
        {
            introDuration = (double)introClip.samples / introClip.frequency;



        }
        audioSource.clip = loopClip;

        PlayIntroLoopStarted?.Invoke();

    }



    void PlayIntro()
    {
        if (introClip == null) return;
        audioSource.PlayOneShot(introClip);
    }

    void PlayLoopScheduled()
    {
        if (loopClip == null) return;
        else if (introClip == null)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.PlayScheduled(AudioSettings.dspTime + introDuration);
        }

    }

    //IEnumerator VolumeUp()
    //{

    //    while (audioSource.volume < 1)
    //    {
    //        audioSource.volume += 0.01f;
    //        yield return new WaitForSeconds(.2f);
    //    }
    //}
}
