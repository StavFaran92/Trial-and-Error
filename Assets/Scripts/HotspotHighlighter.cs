using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class HotspotHighlighter : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private float originalOutlineWidth;
    private Color originalOutlineColor;

    public float highlightOutlineWidth = 20;
    public Color highlightOutlineColor = Color.white;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        originalOutlineWidth = meshRenderer.material.GetFloat("_Outline_Width");
        originalOutlineColor = meshRenderer.material.GetColor("_Outline_Color");
    }

    public void ObjectHighlightOn()
    {
        meshRenderer.material.SetColor("_Outline_Color", highlightOutlineColor);
        meshRenderer.material.SetFloat("_Outline_Width", highlightOutlineWidth);
    }

    public void ObjectHighlightOff()
    {
        meshRenderer.material.SetColor("_Outline_Color", originalOutlineColor);
        meshRenderer.material.SetFloat("_Outline_Width", originalOutlineWidth);
    }
}
