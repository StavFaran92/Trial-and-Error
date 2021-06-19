using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageHotspotHighlighter : MonoBehaviour
{
    private Image image;

    private Color originalOutlineColor;

    public Color highlightOutlineColor = Color.white;

    private void Start()
    {
        image = GetComponent<Image>();

        originalOutlineColor = image.color;
    }

    public void ObjectHighlightOn()
    {
        image.color = highlightOutlineColor;
    }

    public void ObjectHighlightOff()
    {
        image.color = originalOutlineColor;
    }
}
