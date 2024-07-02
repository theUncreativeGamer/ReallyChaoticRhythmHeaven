using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Liquid", menuName = "Beverage/Ingredient/Base Liquid", order = 0)]
public class BaseLiquid : Ingredient
{
    [SerializeField] protected Color color;
    [SerializeField] protected Color containerMainColor;
    [SerializeField] protected Color containerSecondColor;

    public Color Color => color;
    public Color ContainerMainColor => containerMainColor;
    public Color ContainerSecondColor => containerSecondColor;
}
