using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource efxSource;
    public AudioSource efxSource2;

    public AudioClip playerSelectClip;
    public AudioClip gameMusicClip;

    public AudioClip[] attackClips;
    public AudioClip[] impactClips;
    public AudioClip[] deathClips;
    public AudioClip[] hurtClips;
    public AudioClip[] jumpClips;
    public AudioClip[] landingClips;
    public AudioClip[] projectileClips;
    public AudioClip[] tauntClips;

    // singleton
    public MusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void PlayPlayerSelectMusic()
    {
        musicSource.clip = playerSelectClip;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        musicSource.clip = gameMusicClip;
        musicSource.Play();
    }

    public void PlayAttack()
    {
        int index = Random.Range(0, attackClips.Length);
        efxSource.clip = attackClips[index];
        efxSource.Play();
    }

    public void PlayImpact()
    {
        int index = Random.Range(0, impactClips.Length);
        efxSource.clip = impactClips[index];
        efxSource.Play();
    }

    public void PlayDeath()
    {
        int index = Random.Range(0, deathClips.Length);
        efxSource.clip = deathClips[index];
        efxSource.Play();
    }

    public void PlayHurt()
    {
        int index = Random.Range(0, hurtClips.Length);
        efxSource2.clip = hurtClips[index];
        efxSource2.Play();
    }

    public void PlayJump()
    {
        int index = Random.Range(0, jumpClips.Length);
        efxSource.clip = jumpClips[index];
        efxSource.Play();
    }

    public void PlayLanding()
    {
        int index = Random.Range(0, landingClips.Length);
        efxSource.clip = landingClips[index];
        efxSource.Play();
    }

    public void PlayProjectile()
    {
        int index = Random.Range(0, projectileClips.Length);
        efxSource.clip = projectileClips[index];
        efxSource.Play();
    }

    public void PlayTaunt()
    {
        int index = Random.Range(0, tauntClips.Length);
        efxSource.clip = tauntClips[index];
        efxSource.Play();
    }
}
