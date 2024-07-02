using System.Collections.Generic;
using UnityEngine;

public class BeverageBarrelProperty : MonoBehaviour, IOnEnterEditMode, IHasIngredient
{
    [SerializeField] private BaseLiquid baseLiquid;
    [SerializeField] private bool updateColors = false;

    [SerializeField] private List<Renderer> mainColorRenderers = new List<Renderer>();
    [SerializeField] private List<Renderer> secondaryColorRenderers = new List<Renderer>();
    [SerializeField] private Renderer liquidRenderer = null;
    [SerializeField] private SpriteRenderer labelRenderer = null;

    private void OnValidate()
    {
        if (baseLiquid != null && updateColors)
        {
            updateColors = false;
            UpdateRendererColors();
        }
    }

    public void OnEnterEditMode()
    {
        UpdateRendererColors();
    }

    private void Start()
    {
        UpdateRendererColors();
    }

    private void UpdateRendererColors()
    {
        MaterialPropertyBlock mainColorProp = new();
        mainColorProp.SetColor("_Color", baseLiquid.ContainerMainColor);
        foreach (Renderer r in mainColorRenderers)
        {
            r.SetPropertyBlock(mainColorProp);
        }

        MaterialPropertyBlock secondaryColorProp = new();
        secondaryColorProp.SetColor("_Color", baseLiquid.ContainerSecondColor);
        foreach (Renderer r in secondaryColorRenderers)
        {
            r.SetPropertyBlock(secondaryColorProp);
        }

        if (liquidRenderer != null)
        {
            MaterialPropertyBlock liquidColorProp = new();
            liquidColorProp.SetColor("_Color", baseLiquid.Color);
            liquidRenderer.SetPropertyBlock(liquidColorProp, 1);
        }

        if (labelRenderer != null)
        {
            labelRenderer.sprite = baseLiquid.Label;
        }

    }

    public Ingredient GetIngredient()
    {
        return baseLiquid;
    }
}
