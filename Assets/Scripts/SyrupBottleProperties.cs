using UnityEngine;

public class SyrupBottleProperties : MonoBehaviour, IOnEnterEditMode, IHasIngredient
{
    [SerializeField] private Syrup syrup;
    [SerializeField] private bool updateRenderers = false;

    [SerializeField] private Renderer bottleRenderer = null;
    [SerializeField] private Renderer syrupRenderer = null;
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

    private void UpdateRenderers()
    {
        MaterialPropertyBlock mpb = new();
        mpb.SetColor("_Color", syrup.BottleColor);
        if (bottleRenderer != null) bottleRenderer.SetPropertyBlock(mpb);

        mpb.SetColor("_Color", syrup.Color);
        if (syrupRenderer != null) syrupRenderer.SetPropertyBlock(mpb);

        mpb.SetColor("_Color", syrup.LabelColor);
        if (labelRenderer != null) labelRenderer.SetPropertyBlock(mpb, 1);

        if (imageRenderer != null) imageRenderer.sprite = syrup.Label;

        if (syrup.Label == null && placeholderText != null)
            placeholderText.text = syrup.name;
    }
}
