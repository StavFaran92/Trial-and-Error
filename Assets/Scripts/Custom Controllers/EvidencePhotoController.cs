using UnityEngine;
using AC;

public class EvidencePhotoController : MonoBehaviour
{
    [SerializeField] private GameObject questionMark;
    [SerializeField] private GameObject evidencePhoto;

    [SerializeField] private GameObject unknownTextHeadline;
    [SerializeField] private GameObject evidenceTextHeadline;

    [SerializeField] private GameObject unknownTextContent;
    [SerializeField] private GameObject evidenceTextContent;

    [SerializeField] private string isEvidenceRevealedVarLabel;

    private void Start()
    {
        var isEvidenceRevealed = GlobalVariables
            .GetVariable(isEvidenceRevealedVarLabel).BooleanValue;

        questionMark.SetActive(!isEvidenceRevealed);
        unknownTextHeadline.SetActive(!isEvidenceRevealed);
        unknownTextContent.SetActive(!isEvidenceRevealed);

        evidencePhoto.SetActive(isEvidenceRevealed);
        evidenceTextHeadline.SetActive(isEvidenceRevealed);
        evidenceTextContent.SetActive(isEvidenceRevealed);
    }
}
