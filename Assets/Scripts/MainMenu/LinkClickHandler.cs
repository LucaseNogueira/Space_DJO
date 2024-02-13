using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkClickHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text textMeshPro;

    private readonly Color linkDefaultColor = Color.blue;
    private readonly Color linkClickedColor = Color.red;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, null);
        if (linkIndex != -1)
        {
            ResetLinkColors();

            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];

            GUIUtility.systemCopyBuffer = linkInfo.GetLinkText();

            string oldLinkText = linkInfo.GetLinkText();
            string newLinkText = $"<color=#{ColorUtility.ToHtmlStringRGB(linkClickedColor)}>{oldLinkText}</color>";
            textMeshPro.text = textMeshPro.text.Replace(oldLinkText, newLinkText);
        }
    }

    private void ResetLinkColors()
    {
        foreach (TMP_LinkInfo linkInfo in textMeshPro.textInfo.linkInfo)
        {
            string oldLinkText = linkInfo.GetLinkText();
            string newLinkText = $"<color=#{ColorUtility.ToHtmlStringRGB(linkDefaultColor)}>{oldLinkText}</color>";
            textMeshPro.text = textMeshPro.text.Replace(oldLinkText, newLinkText);
        }
    }
}
