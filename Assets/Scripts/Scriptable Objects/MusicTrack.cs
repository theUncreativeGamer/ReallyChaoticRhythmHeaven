using UnityEngine;

/// <summary>
/// Stores all the info about a music track, like the music clip itself, the BPM, 
/// and how the beat pattern complexity changes as the track progresses.
/// </summary>
[CreateAssetMenu(fileName = "New Music Track", menuName = "Custom/Music Track")]
public class MusicTrack : ScriptableObject
{
    [SerializeField] private AudioClip track;
    [SerializeField] private float bpm;
    [SerializeField] private int measureLength;
    [SerializeField] private int measureCount;
    [SerializeField] private BeatPatternData availableBeatPatterns;
    [SerializeField] private AvailableIngredientsTable availableIngredients;
    [SerializeField] private int[] beatPatternComplexities;

    [SerializeField, HideInInspector] private TextAsset beatPatternTextAsset;

    public AudioClip Track { get => track; private set => track = value; }
    public float Bpm { get => bpm; private set => bpm = value; }
    public int MeasureLength { get => measureLength; private set => measureLength = value; }
    public int MeasureCount { get => measureCount; private set => measureCount = value; }
    public BeatPatternData AvailableBeatPatterns { get => availableBeatPatterns; private set => availableBeatPatterns = value; }
    public AvailableIngredientsTable AvailableIngredients { get => availableIngredients; private set => availableIngredients = value; }

    /// <summary>
    /// How many beats there are in each measure of the track.
    /// </summary>
    public int[] BeatPatternComplexities { get => beatPatternComplexities; private set => beatPatternComplexities = value; }

    // Method to set beat pattern complexities from a text file
    public void SetBeatPatternComplexities(int[] complexities)
    {
        beatPatternComplexities = complexities;
    }
}
