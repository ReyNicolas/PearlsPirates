using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI playerPointsText;
    [SerializeField] Image shipImage;

    public void Initialize(PlayerSO playerData)
    {
        playerNameText.text = playerData.PlayerName;
        playerPointsText.color = playerData.PlayerColor;
        playerPointsText.text = playerData.PointsToAdd.Value.ToString();
        shipImage.sprite = playerData.shipSprite;
    }
}

