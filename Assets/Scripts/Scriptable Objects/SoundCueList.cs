using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundCue
{
    public Ingredient ingredient;
    public float cueTime;
}

/// <summary>
/// A series of cues that will be played in a measure.
/// </summary>
[CreateAssetMenu(menuName = "Sound Cue List")]
public class SoundCueList : ScriptableObject
{
    public List<SoundCue> CueList = new();
}
