using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(AudioSource))]
public class BeatPlayer : MonoBehaviour
{
    [SerializeField]
    private FloatVariable BeatsPerMinute;

    [SerializeField]
    private List<AudioClip> BeatSounds;

    [SerializeField]
    private AudioSource AudioSource;

    private float NextBeatTime = 0f;
    private int SoundIndex = 0;
    private bool PlayingEnabled = false;

    private void Start()
    {
        PlayBeat();
        PlayingEnabled = true;
    }

    private void Update()
    {
        if(PlayingEnabled && Time.time >= NextBeatTime)
            PlayBeat();
    }

    private void OnValidate()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void PlayBeat()
    {
        AudioSource.clip = NextSound();
        AudioSource.Play();
        
        NextBeatTime = NextBeatTime + GetBeatInterval();
    }

    private AudioClip NextSound()
    {
        AudioClip beatSound = BeatSounds[SoundIndex];
        SoundIndex = SoundIndex >= BeatSounds.Count - 1 ? 0 : SoundIndex + 1;

        return beatSound;
    }

    private float GetBeatInterval()
    {
        return 60.0f / BeatsPerMinute.Value;
    }
}
