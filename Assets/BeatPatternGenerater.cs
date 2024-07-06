using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPatternGenerater : MonoBehaviour
{
    public MusicTrack musicTrackInfo;
    public BeatPatternData beatPatternData;
    public AvailableIngredientsTable availableIngredientsTable;
    private Beverage[] beverages;
    private SoundCueList[] soundCueLists;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (musicTrackInfo != null && beatPatternData != null && availableIngredientsTable != null)
        {
            beverages = new Beverage[musicTrackInfo.MeasureCount];
            soundCueLists = new SoundCueList[musicTrackInfo.MeasureCount];
            for (int i = 0; i < musicTrackInfo.MeasureCount; i++)
            {
                if (musicTrackInfo.BeatPatternComplexities[i] == 0)
                {
                    beverages[i] = null;
                    soundCueLists[i] = null;
                }
                else
                {
                    int beatCount = musicTrackInfo.BeatPatternComplexities[i];
                    beverages[i] = availableIngredientsTable.RandomBeverage(new Range<int>(1, 3), beatCount);
                    soundCueLists[i] = beverages[i].ToCueList(beatPatternData.GetRandomPattern(beatCount));
                }

            }
        }
    }

    public void StartMeasureEvent(int measure)
    {
        SendCues(soundCueLists[measure]);
    }

    private void SendCues(SoundCueList cueList)
    {
        if (cueList == null) return;
        foreach (var cue in cueList.CueList)
        {
            StartCoroutine(PlayCue(cue.ingredient.GetCue(), cue.cueTime * (60f/musicTrackInfo.Bpm)));
        }
    }

    private IEnumerator PlayCue(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);

        audioSource.clip = clip;
        audioSource.Play();
    }
}
