using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Syrup", menuName = "Beverage/Ingredient/Syrup", order = 1)]
public class Syrup : Ingredient
{
    [SerializeField] protected Color color;
    [SerializeField] protected Color bottleColor;
    [SerializeField] protected Color labelColor;

    public Color Color => color;
    public Color BottleColor => bottleColor;
    public Color LabelColor => labelColor;
}
