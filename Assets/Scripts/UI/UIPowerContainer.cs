using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerContainer : MonoBehaviour
{
    [SerializeField] Image powerImage;
    [SerializeField] TextMeshProUGUI descriptionText;

    public void Initialize(PowerSO powerData)
    {
        powerImage.sprite = powerData.sprite;
        descriptionText.text = powerData.Description;
    }
}
