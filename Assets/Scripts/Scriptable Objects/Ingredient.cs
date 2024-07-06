using System;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : ScriptableObject, IComparable<Ingredient>, IHasIcon
{
    [SerializeField] protected Sprite label;
    [SerializeField] protected List<AudioClip> soundCues = new();

    public Sprite Label => label;
    public List<AudioClip> SoundCues => soundCues;

    public int CompareTo(Ingredient other)
    {
        if (other == null) return 1;

        return name.CompareTo(other.name);
    }

    public Sprite GetIcon()
    {
        return label;
    }

    public string GetPlaceholderText()
    {
        return name;
    }

    public AudioClip GetCue()
    {
        if(soundCues.Count == 0) return null;
        return soundCues.GetRandom();
    }
}
