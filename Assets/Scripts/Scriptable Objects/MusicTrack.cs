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
    [SerializeField] private TextAsset dialogueAsset;
    [SerializeField] private int maxScore;
    /// <summary>
    /// In case if the song doesn't start from 0:00, the triggered time of the StartMeasureEvent is moved by this value seconds.
    /// </summary>
    [SerializeField] private float offset = 0;
    [SerializeField] private BeatPatternData availableBeatPatterns;
    [SerializeField] private AvailableIngredientsTable availableIngredients;
    [SerializeField] private int[] beatPatternComplexities;
    /// <summary>
    /// The first value of each pair is the measure count the break comes after.
    /// The second value is how many beats the break take.
    /// </summary>
    [SerializeField] private Pair<int, float>[] breakTimes = new Pair<int, float>[0];

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
    public float Offset { get => offset; private set => offset = value; }
    public TextAsset DialogueAsset { get => dialogueAsset; private set => dialogueAsset = value; }
    public int MaxScore { get => maxScore; private set => maxScore = value; }
    public Pair<int, float>[] BreakTimes { get => breakTimes; private set => breakTimes = value; }

    // Method to set beat pattern complexities from a text file
    public void SetBeatPatternComplexities(int[] complexities)
    {
        beatPatternComplexities = complexities;
        int totalIngredientCount = 0;
        foreach (int complexity in complexities)
            totalIngredientCount += complexity;
        maxScore = totalIngredientCount * 10;
    }
}
