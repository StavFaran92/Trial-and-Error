using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text uiText;
    private Color originalTextColor;

    public Color highlightedTextColor = Color.white;

    private void Start()
    {
        uiText = GetComponent<Text>();
        originalTextColor = uiText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiText.color = highlightedTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiText.color = originalTextColor;
    }
}
