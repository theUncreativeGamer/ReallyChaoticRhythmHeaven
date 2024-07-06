using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the possible beat patterns the game can generate.
/// </summary>
[CreateAssetMenu(fileName = "BeatPatternData", menuName = "ScriptableObjects/BeatPatternData", order = 1)]
public class BeatPatternData : ScriptableObject
{
    public List<BeatPatternCategory> categories = new();
    public BeatPattern GetRandomPattern(int length)
    {
        return categories.Find(c => c.length == length).patterns.GetRandom();
    }
}

/// <summary>
/// All the beat patterns with a specific length.
/// </summary>
[System.Serializable]
public class BeatPatternCategory
{
    public int length;
    public List<BeatPattern> patterns = new();
}

/// <summary>
/// A beat pattern is a series of floats that represent which beats the cue will occur.
/// </summary>
[System.Serializable]
public class BeatPattern
{
    [SerializeField] private float[] _array;

    public BeatPattern(int size)
    {
        _array = new float[size];
    }

    public float this[int index]
    {
        get => _array[index];
        set => _array[index] = value;
    }

    public int Length => _array.Length;

    // Other methods or properties if needed
}

