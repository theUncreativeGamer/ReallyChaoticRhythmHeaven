using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundCue
{
    public AudioClip audioClip;
    public float cueTime;
}

[CreateAssetMenu(menuName = "Sound Cue List")]
public class SoundCueList : ScriptableObject
{
    public List<SoundCue> CueList = new();
}
