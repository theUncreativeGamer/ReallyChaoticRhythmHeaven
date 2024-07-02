using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public Beverage currentBeverage;
    [SerializeField] private IconListDisplay baseLiquidsDisplay;
    [SerializeField] private IconListDisplay syrupsDisplay;
    [SerializeField] private IconListDisplay sidesDisplay;

    public bool AddIngredientToCurrentBeverage(Ingredient ingredient)
    {
        bool result = currentBeverage.Add(ingredient);
        if (result)
        {
            baseLiquidsDisplay.Icons = IHasIconContainer.ConvertList(currentBeverage.baseLiquids);
            syrupsDisplay.Icons = IHasIconContainer.ConvertList(currentBeverage.syrups);
            sidesDisplay.Icons = IHasIconContainer.ConvertList(currentBeverage.sideIngredients);
            return true;
        }

        return false;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        currentBeverage = ScriptableObject.CreateInstance<Beverage>();
    }
}
