using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Beverage/Available Ingredients Table")]
public class AvailableIngredientsTable : ScriptableObject
{
    [SerializeField] private List<BaseLiquid> _availableLiquids;
    [SerializeField] private List<Syrup> _availableSyrups;
    [SerializeField] private List<SideIngredient> _availableSideIngredients;

    public List<BaseLiquid> AvailableLiquids => _availableLiquids;
    public List<Syrup> AvailableSyrups => _availableSyrups;
    public List<SideIngredient> AvailableSideIngredients => _availableSideIngredients;

    public Beverage RandomBeverage(Range<int> liquidCountRange, int totalIngredientCount)
    {
        Beverage newBev = ScriptableObject.CreateInstance<Beverage>();

        int liquidCount = Random.Range(liquidCountRange.min, liquidCountRange.max + 1);
        int syrupCount = Random.Range(0, totalIngredientCount - liquidCount + 1);
        int sideCount = totalIngredientCount - liquidCount - syrupCount;

        if (liquidCount < 1) 
        { 
            liquidCount = 1;
            Debug.LogError("U cannot make beverage with no liquid >u>");
        }

        for(int i = 0;i< liquidCount;i++) 
        {
            newBev.Add(_availableLiquids.GetRandom());
        }

        for (int i = 0; i < syrupCount; i++)
        {
            newBev.Add(_availableSyrups.GetRandom());
        }

        for (int i = 0; i < sideCount; i++)
        {
            newBev.Add(_availableSideIngredients.GetRandom());
        }
         
        return newBev;
    }
}
