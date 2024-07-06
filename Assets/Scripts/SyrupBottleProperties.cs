using System.Collections.Generic;
using UnityEngine;

public class SyrupBottleProperties : MonoBehaviour, IOnEnterEditMode, IHasIngredient
{
    [SerializeField] public Syrup syrup;
    [SerializeField] private bool updateRenderers = false;

    [SerializeField] private Renderer bottleRenderer = null;
    [SerializeField] private List<Renderer> syrupRenderers = new();
    [SerializeField] private Renderer labelRenderer = null;
    [SerializeField] private SpriteRenderer imageRenderer = null;
    [SerializeField] private TMPro.TextMeshPro placeholderText = null;

    public Ingredient GetIngredient()
    {
        return syrup;
    }

    public void OnEnterEditMode()
    {
        UpdateRenderers();
    }

    private void OnValidate()
    {
        if(updateRenderers)
        {
            updateRenderers = false;
            UpdateRenderers();
        }
    }

    private void Start()
    {
        UpdateRenderers();
    }

    public void UpdateRenderers()
    {
        if(syrup == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        MaterialPropertyBlock mpb = new();
        mpb.SetColor("_Color", syrup.BottleColor);
        if (bottleRenderer != null) bottleRenderer.SetPropertyBlock(mpb);

        mpb.SetColor("_Color", syrup.Color);
        if (syrupRenderers != null) 
            foreach (Renderer renderer in syrupRenderers)
                renderer.SetPropertyBlock(mpb);

        mpb.SetColor("_Color", syrup.LabelColor);
        if (labelRenderer != null) labelRenderer.SetPropertyBlock(mpb, 1);

        if (imageRenderer != null) imageRenderer.sprite = syrup.Label;

        if (syrup.Label == null && placeholderText != null)
            placeholderText.text = syrup.name;
    }
}
