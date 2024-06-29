using System.Collections.Generic;
using UnityEngine;

public class BeverageBarrelProperty : MonoBehaviour
{
    [SerializeField] private BaseLiquid baseLiquid;
    [SerializeField] private bool updateColors = false;
    private Color _originalColor = Color.white;

    [SerializeField] private List<Renderer> mainColorRenderers = new List<Renderer>();
    [SerializeField] private List<Renderer> secondaryColorRenderers = new List<Renderer>();
    [SerializeField] private Renderer liquidRenderer = null;
    [SerializeField] private SpriteRenderer labelRenderer = null;

    private void OnValidate()
    {
        if (baseLiquid != null && (updateColors || baseLiquid.Color != _originalColor))
        {
            updateColors = false;
            _originalColor = baseLiquid.Color;
            UpdateRendererColors();
        }
    }

    private void Start()
    {
        UpdateRendererColors();
    }

    private void UpdateRendererColors()
    {
        MaterialPropertyBlock mainColorProp = new MaterialPropertyBlock();
        mainColorProp.SetColor("_Color", Color.Lerp(baseLiquid.Color, Color.gray, 0.3f));
        foreach (Renderer r in mainColorRenderers)
        {
            r.SetPropertyBlock(mainColorProp);
        }

        MaterialPropertyBlock secondaryColorProp = new MaterialPropertyBlock();
        secondaryColorProp.SetColor("_Color", Color.Lerp(baseLiquid.Color, Color.white, 0.6f));
        foreach (Renderer r in secondaryColorRenderers)
        {
            r.SetPropertyBlock(secondaryColorProp);
        }

        if (liquidRenderer != null)
        {
            MaterialPropertyBlock liquidColorProp = new MaterialPropertyBlock();
            Color liquidColor = Color.Lerp(baseLiquid.Color, Color.white, 0.2f);
            liquidColor.a = 0.8f;
            liquidColorProp.SetColor("_Color", liquidColor);
            liquidRenderer.SetPropertyBlock(liquidColorProp, 1);
        }

        if (labelRenderer != null)
        {
            labelRenderer.sprite = baseLiquid.Label;
        }

    }
}
