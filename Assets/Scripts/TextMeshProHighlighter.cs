using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private TextMeshProUGUI text;
    private Color originalTextColor;

    public Color highlightedTextColor = Color.white;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalTextColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = highlightedTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = originalTextColor;
    }
}

