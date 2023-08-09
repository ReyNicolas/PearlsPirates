using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomePlayerPanel : MonoBehaviour
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] TextMeshProUGUI playerName;

    private void Start()
    {
        playerName.text = playerData.PlayerName;
        playerName.color = playerData.PlayerColor;
    }
}
