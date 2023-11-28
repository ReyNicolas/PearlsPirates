using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomePlayerPanel : MonoBehaviour
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] Image shipImage;
    [SerializeField] Image inputImage;

    public void SetMyPlayer(PlayerSO playerData)
    {
        this.playerData = playerData;
        playerName.text = playerData.PlayerName;
        playerName.color = playerData.PlayerColor;
        Debug.Log(playerData.InputDevice);
        shipImage.sprite = playerData.shipSprite;
        inputImage.sprite = Resources.Load<Sprite>(playerData.InputDevice);

    }
}
