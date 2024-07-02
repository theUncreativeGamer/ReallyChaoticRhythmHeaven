using Assets.IUnified.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IHasIcon
{
    public Sprite GetIcon();
    public string GetPlaceholderText();
}

[Serializable]
public class IHasIconContainer : IUnifiedContainer<IHasIcon> 
{
    static public List<IHasIconContainer> ConvertList<T>(List<T> originalList) where T : IHasIcon
    {
        return originalList.Select(obj => new IHasIconContainer { Result = obj }).ToList();
    }
}
