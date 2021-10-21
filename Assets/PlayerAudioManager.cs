using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip gravityChangeSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip coinPickUpSound;
    [SerializeField] AudioClip livePickupSound;
    [SerializeField] AudioClip liveGainSound;

    double clipStarted;

    private AudioClip currentAudioClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null) return;

        else if (currentAudioClip == audioClip && audioClip == deathSound)
        {
            return;
        }


        else if (audioClip == currentAudioClip)
        {
            if (AudioSettings.dspTime > clipStarted + currentAudioClip.length / 2)
            {
                audioSource?.Stop();
                audioSource?.PlayOneShot(audioClip);
                currentAudioClip = audioClip;
            }
            else return;

        }

        else
        {
            audioSource?.PlayOneShot(audioClip);
            currentAudioClip = audioClip;
        }

        clipStarted = AudioSettings.dspTime;
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayGravityChangeSound()
    {
        PlaySound(gravityChangeSound);
    }

    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }

    public void PlayDeathSound()
    {
        PlaySound(deathSound);
    }

    public void PlayCoinPickUpSound()
    {
        PlaySound(coinPickUpSound);
    }

    public void PlayLivePickUpSound()
    {
        PlaySound(livePickupSound);
    }

    public void PlayLiveGainSound()
    {
        PlaySound(liveGainSound);
    }

}
