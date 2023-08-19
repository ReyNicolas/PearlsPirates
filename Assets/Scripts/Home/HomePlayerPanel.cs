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

    private void Start()
    {
        playerName.text = playerData.PlayerName;
        playerName.color = playerData.PlayerColor;
        shipImage.sprite = playerData.shipSprite;
    }
}