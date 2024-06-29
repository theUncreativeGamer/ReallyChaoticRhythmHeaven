using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Side Ingredient", menuName = "Beverage Ingredient/Side Ingredient", order = 2)]
public class SideIngredient : Ingredient
{
    [SerializeField] protected Color color;

    public Color Color => color;
}
