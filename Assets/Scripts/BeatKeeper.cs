using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeatKeeper : MonoBehaviour, ILoadMusicTrack
{
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private MusicTrack musicTrack;
    /// <summary>
    /// In case if the song doesn't start from 0:00, the triggered time of the StartMeasureEvent is moved by this value seconds.
    /// </summary>
    [SerializeField] private float offset = 0;
    public bool useMetronome = false;
    public SoundCueList metronomeAsset;

    [Header("Debug Don't Edit")]
    [SerializeField] private float beatLength;
    [SerializeField] private float measureLength;
    [SerializeField] private int lastMeasure = -1;
    [SerializeField] private float totalBreakTime = 0;
    [SerializeField] private int nextBreakIndex = 0;
    private AudioSource audioSource;
    public bool IsPlayingMusic { get; private set; }

    public void LoadMusicTrack(MusicTrack track)
    {
        musicTrack = track;
        musicPlayer.clip = track.Track;

        beatLength = 60f / musicTrack.Bpm;
        measureLength = beatLength * musicTrack.MeasureLength;
        offset = track.Offset;
    }

    public bool StartPlayingMusic()
    {
        if(IsPlayingMusic)
        {
            Debug.LogWarning("The music is already playing!");
            return false;
        }
        IsPlayingMusic = true;

        musicPlayer.Play();
        //BroadcastMessage("StartMeasureEvent", lastMeasure, SendMessageOptions.DontRequireReceiver);
        //SendCues(metronomeAsset);
        return true;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!IsPlayingMusic) return;

        if(!musicPlayer.isPlaying ) 
        {
            IsPlayingMusic=false;
            GameManager.Instance.OnGameEnd();
            return;
        }

        float currentMeasureCount = ((float)musicPlayer.timeSamples / musicPlayer.clip.frequency - offset - totalBreakTime) / measureLength;
        
        if (Mathf.FloorToInt(currentMeasureCount) > lastMeasure)
        {
            lastMeasure = Mathf.FloorToInt(currentMeasureCount);
            if (nextBreakIndex < musicTrack.BreakTimes.Length && Mathf.FloorToInt(currentMeasureCount) == musicTrack.BreakTimes[nextBreakIndex].First)
            {
                totalBreakTime += musicTrack.BreakTimes[nextBreakIndex].Second * (60f / musicTrack.Bpm);
                nextBreakIndex++;
            }
            BroadcastMessage("StartMeasureEvent", lastMeasure, SendMessageOptions.DontRequireReceiver);
            if(useMetronome)
                SendCues(metronomeAsset);
        }
    }

    private void SendCues(SoundCueList cueList)
    {
        foreach (var cue in cueList.CueList)
        {
            StartCoroutine(PlayCue(cue.ingredient.GetCue(), cue.cueTime * beatLength));
        }
    }

    private IEnumerator PlayCue(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);

        audioSource.clip = clip;
        audioSource.Play();
    }

}
