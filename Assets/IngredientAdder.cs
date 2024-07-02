using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IHasIngredient))]
public class IngredientAdder : MonoBehaviour, IInteractable
{
    private IHasIngredient _ingredient;
    private void Awake()
    {
        _ingredient = GetComponent<IHasIngredient>();
    }
    public void Interact()
    {
        GameManager.Instance.AddIngredientToCurrentBeverage(_ingredient.GetIngredient());
    }
}
