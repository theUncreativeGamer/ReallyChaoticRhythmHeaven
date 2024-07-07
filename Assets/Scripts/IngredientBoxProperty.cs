using System.Collections.Generic;
using UnityEngine;

public class IngredientBoxProperty : MonoBehaviour, IOnEnterEditMode, IHasIngredient
{
    [SerializeField] public SideIngredient sideIngredient;
    [SerializeField] private bool updateRenderers = false;

    [SerializeField] private List<Renderer> stuffRenderers;
    [SerializeField] private List<SpriteRenderer> labelRenderers = new();
    [SerializeField] private List<TMPro.TextMeshPro> placeholderTexts = new();

    private void OnValidate()
    {
        if (sideIngredient != null && updateRenderers)
        {
            updateRenderers = false;
            UpdateRenderers();
        }
    }

    public void OnEnterEditMode()
    {
        UpdateRenderers();
    }

    private void Start()
    {
        UpdateRenderers();
    }

    public void UpdateRenderers()
    {
        if (sideIngredient == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        MaterialPropertyBlock stuffProp = new();
        stuffProp.SetColor("_Color", sideIngredient.Color);
        foreach (Renderer r in stuffRenderers)
            r.SetPropertyBlock(stuffProp);

        foreach (var v in labelRenderers)
        {
            v.sprite = sideIngredient.Label;
        }

        if (sideIngredient.Label == null)
            foreach (var v in placeholderTexts)
            {
                v.text = sideIngredient.name;
            }
        else foreach (var v in placeholderTexts)
            {
                v.text = "";
            }

    }

    public Ingredient GetIngredient()
    {
        return sideIngredient;
    }
}
