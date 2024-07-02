using UnityEngine;

[CreateAssetMenu(fileName = "New Side Ingredient", menuName = "Beverage/Ingredient/Side Ingredient", order = 2)]
public class SideIngredient : Ingredient
{
    [SerializeField] protected Color color;
    [SerializeField] protected Material material;

    public Color Color => color;
    public Material Material => material;
}
