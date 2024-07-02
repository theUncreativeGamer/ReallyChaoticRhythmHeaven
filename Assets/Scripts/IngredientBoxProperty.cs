using System.Collections.Generic;
using UnityEngine;

public class IngredientBoxProperty : MonoBehaviour, IOnEnterEditMode, IHasIngredient
{
    [SerializeField] private SideIngredient sideIngredient;
    [SerializeField] private bool updateRenderers = false;

    [SerializeField] private Renderer stuffRenderer;
    [SerializeField] private List<SpriteRenderer> labelRenderers = new();
    [SerializeField] private List<TMPro.TextMeshPro> placeholderTexts = new();

    private void OnValidate()
    {
        if (sideIngredient != null && updateRenderers)
        {
            updateRenderers = false;
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
        MaterialPropertyBlock stuffProp = new();
        stuffProp.SetColor("_Color", sideIngredient.Color);
        stuffRenderer.SetPropertyBlock(stuffProp);

        foreach(var v in labelRenderers)
        {
            v.sprite = sideIngredient.Label;
        }

        if(sideIngredient.Label == null)
            foreach(var v in placeholderTexts)
            {
                v.text = sideIngredient.name;
            }

    }

    public Ingredient GetIngredient()
    {
        return sideIngredient;
    }
}
