using System;
using UnityEngine;

public class Ingredient : ScriptableObject, IComparable<Ingredient>, IHasIcon
{
    [SerializeField] protected Sprite label;
    [SerializeField] protected AudioClip soundCue;

    public Sprite Label => label;
    public AudioClip SoundCue => soundCue;

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
}
