using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeatKeeper : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;
    public float bpm;
    public float totalBeatInMeasure = 4;
    public bool useMetronome = false;
    public SoundCueList metronomeAsset;

    [Header("Debug Don't Edit")]
    [SerializeField] private float beatLength;
    [SerializeField] private float measureLength;
    [SerializeField] private int lastMeasure = 0;
    private AudioSource audioSource;
    public bool IsPlayingMusic { get; private set; }

    public bool StartPlayingMusic()
    {
        if(IsPlayingMusic)
        {
            Debug.LogWarning("The music is already playing!");
            return false;
        }
        IsPlayingMusic = true;

        beatLength = 60f / bpm;
        measureLength = beatLength * totalBeatInMeasure;
        musicPlayer.Play();
        //SendCues(metronomeAsset);
        return true;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!IsPlayingMusic) return;
        float currentMeasureCount = musicPlayer.timeSamples / (musicPlayer.clip.frequency * measureLength);
        if(Mathf.FloorToInt(currentMeasureCount) != lastMeasure)
        {
            lastMeasure = Mathf.FloorToInt(currentMeasureCount);
            SendCues(metronomeAsset);
        }
    }

    private void SendCues(SoundCueList cueList)
    {
        foreach (var cue in cueList.CueList)
        {
            StartCoroutine(PlayCue(cue.audioClip, cue.cueTime * beatLength));
        }
    }

    private IEnumerator PlayCue(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);

        audioSource.clip = clip;
        audioSource.Play();
    }

}
