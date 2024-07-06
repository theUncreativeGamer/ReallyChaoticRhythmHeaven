using System;
using System.Collections.Generic;
using UnityEngine;

public class Beverage : ScriptableObject
{
    public List<BaseLiquid> baseLiquids = new List<BaseLiquid>();
    public List<Syrup> syrups = new List<Syrup>();
    public List<SideIngredient> sideIngredients = new List<SideIngredient>();

    private int ingredientCount = 0;
    public int IngredientCount { get => ingredientCount; }

    public bool Add(Ingredient ingredient)
    {
        if (ingredient is BaseLiquid)
        {
            baseLiquids.Add(ingredient as BaseLiquid);
            ingredientCount++;
            return true;
        }
        else if (ingredient is Syrup)
        {
            syrups.Add(ingredient as Syrup);
            ingredientCount++;
            return true;
        }
        else if (ingredient is SideIngredient)
        {
            sideIngredients.Add(ingredient as SideIngredient);
            ingredientCount++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public SoundCueList ToCueList(BeatPattern pattern)
    {
        if (ingredientCount != pattern.Length)
        {
            Debug.LogError("The ingredientCount doesn't match pattern.Length.");
            return null;
        }

        //Debug.Log("Creating a cue list of length: " + ingredientCount);

        SoundCueList result = ScriptableObject.CreateInstance<SoundCueList>();
        int index = 0;

        foreach (SideIngredient stuff in sideIngredients)
        {
            result.CueList.Add(new SoundCue() { cueTime = pattern[index], ingredient = stuff });
            index++;
        }

        foreach (BaseLiquid stuff in baseLiquids)
        {
            result.CueList.Add(new SoundCue() { cueTime = pattern[index], ingredient = stuff });
            index++;
        }

        foreach (Syrup stuff in syrups)
        {
            result.CueList.Add(new SoundCue() { cueTime = pattern[index], ingredient = stuff });
            index++;
        }

        
        //Debug.Log("and the cue list indeed has " + index + " elements.");
        return result;
    }

    public static bool operator ==(Beverage lhs, Beverage rhs)
    {
        // If both are null, or both are the same instance, return true.
        if (ReferenceEquals(lhs, rhs))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if ((object)lhs == null || (object)rhs == null)
        {
            return false;
        }

        // Return true if the fields match:
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Beverage lhs, Beverage rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Beverage other = (Beverage)obj;
        return Equals(other);
    }

    private bool Equals(Beverage other)
    {
        return MyUtilities.ListsContainSameElements(baseLiquids, other.baseLiquids)
            && MyUtilities.ListsContainSameElements(syrups, other.syrups)
            && MyUtilities.ListsContainSameElements(sideIngredients, other.sideIngredients);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), name, hideFlags, baseLiquids, syrups, sideIngredients);
    }
}
