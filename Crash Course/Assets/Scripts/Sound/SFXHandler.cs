using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SFXHandler : MonoBehaviour
{
    public static float masterVolume { get { return PlayerPrefs.GetFloat("MasterVolume", 1.0f); } }
    public static float sfxVolume { get { return PlayerPrefs.GetFloat("SFXVolume", 0.5f); } }

    [SerializeField]
    AudioClip buttonSFX;
    [SerializeField]
    AudioClip damageSFX;

    AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void Update()
    {
        audioSource.volume = masterVolume * sfxVolume;
    }

    public void PlayButtonSFX()
    {
        audioSource.clip = buttonSFX;
        audioSource.Play();
    }

    public void PlayDamageSound()
    {
        audioSource.clip = damageSFX;
        audioSource.Play();
    }
}