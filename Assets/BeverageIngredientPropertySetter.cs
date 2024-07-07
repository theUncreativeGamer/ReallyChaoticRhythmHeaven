using UnityEngine;

public class BeverageIngredientPropertySetter : MonoBehaviour, ILoadMusicTrack
{
    [SerializeField] private MusicTrack musicTrack;
    [SerializeField] private BeverageBarrelProperty[] beverageBarrelProperties;
    [SerializeField] private SyrupBottleProperties[] syrupBottleProperties;
    [SerializeField] private IngredientBoxProperty[] ingredientBoxProperties;

    public void LoadMusicTrack(MusicTrack track)
    {
        musicTrack = track;
        for (int i = 0; i < beverageBarrelProperties.Length; i++)
        {
            if (i < track.AvailableIngredients.AvailableLiquids.Count)
            {
                beverageBarrelProperties[i].baseLiquid = track.AvailableIngredients.AvailableLiquids[i];
            }
            else
            {
                beverageBarrelProperties[i].baseLiquid = null;
            }

            beverageBarrelProperties[i].UpdateRenderers();
        }

        for (int i = 0; i < syrupBottleProperties.Length; i++)
        {
            if (i < track.AvailableIngredients.AvailableSyrups.Count)
            {
                syrupBottleProperties[i].syrup = track.AvailableIngredients.AvailableSyrups[i];
            }
            else
            {
                syrupBottleProperties[i].syrup = null;
            }

            syrupBottleProperties[i].UpdateRenderers();
        }

        for (int i = 0; i < ingredientBoxProperties.Length; i++)
        {
            if (i < track.AvailableIngredients.AvailableSideIngredients.Count)
            {
                ingredientBoxProperties[i].sideIngredient = track.AvailableIngredients.AvailableSideIngredients[i];
            }
            else
            {
                ingredientBoxProperties[i].sideIngredient = null;
            }

            ingredientBoxProperties[i].UpdateRenderers();
        }
    }
}
