using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Sprite defaultNotPowerSprite;
    [SerializeField] GameObject pearlCollectedPrefab;
    [SerializeField] List<Image> powersImages;
    [SerializeField] Canvas ScreenCanvas;
    [SerializeField] TextMeshProUGUI playerPoints;
    [SerializeField] PlayerSO playerData;

    private void Start()
    {
        playerData.powersInCollectors
         .ObserveReplace()
         .Subscribe(powerSO => SetPowerImage(powerSO.Key, powerSO.NewValue));

        playerData.PointsToAdd
            .Subscribe(value=> UpdatePlayerPointsText(value));

        playerData.pearlsCollectedDatas
            .ObserveAdd()
            .Subscribe(powerCollected=> ShowPearlCollected(powerCollected.Value));

        SetPlayerColor(GetComponent<Image>(), playerData.PlayerColor);
    }

    void ShowPearlCollected(PowerSO power)
    {
        var pearlGO = Instantiate(pearlCollectedPrefab, ScreenCanvas.transform);
        pearlGO.transform.position = playerData.position;
        pearlGO.GetComponent<Image>().color = power.PowerColor;
    }

   

    void UpdatePlayerPointsText(int value)
    {
        playerPoints.text = value.ToString();
    }

    void SetPowerImage(int id, PowerSO power) 
        => powersImages[id].sprite = (power != null) ? power.sprite : defaultNotPowerSprite;
    void SetPlayerColor(Image panelImage, Color playerColor)
    {
        panelImage.color = new Color(playerColor.r, playerColor.g, playerColor.b, 0.75f);
        playerPoints.color = new Color(playerColor.r, playerColor.g, playerColor.b, 0.75f);
    }
}
