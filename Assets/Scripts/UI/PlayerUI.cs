using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Sprite defaultNotPowerSprite;
    [SerializeField] GameObject pearlCollectedPrefab;
    [SerializeField] Canvas ScreenCanvas;
    [SerializeField] PlayerSO playerData;
    [SerializeField] MatchSO matchData;
    [Header("Player Info")]
    [SerializeField] List<Image> powersImages;
    [SerializeField] TextMeshProUGUI playerPoints;
    CompositeDisposable disposables;


    private void OnEnable()
    {
        disposables = new CompositeDisposable(
            playerData.powersInCollectors
             .ObserveReplace()
             .Subscribe(powerSO => SetPowerImage(powerSO.Key, powerSO.NewValue)),

            playerData.PointsToAdd
                .Subscribe(value => UpdatePlayerPointsText(value)),

            playerData.pearlsCollectedDatas
                .ObserveAdd()
                .Subscribe(powerCollected => ShowPearlCollected(powerCollected.Value))            
            );
    }

   
    private void Start()
    {  
        SetPlayerColor(GetComponent<Image>(), playerData.PlayerColor);
    }

    private void OnDisable()
    {
        disposables.Dispose();
    }

    void ShowPearlCollected(PowerSO power)
    {
        var pearlGO = Instantiate(pearlCollectedPrefab, ScreenCanvas.transform);
        pearlGO.transform.position = playerData.position;
        pearlGO.GetComponent<Image>().color = power.PowerColor;
    }



    void UpdatePlayerPointsText(int value)
    {
        playerPoints.text = value.ToString() +" / " + matchData.totalPlayerPointsLimit.ToString();
    }

    void SetPowerImage(int id, PowerSO power) 
        => powersImages[id].sprite = (power != null) ? power.sprite : defaultNotPowerSprite;
    void SetPlayerColor(Image panelImage, Color playerColor)
    {
        panelImage.color = new Color(playerColor.r, playerColor.g, playerColor.b, 0.75f);
        //playerPoints.color = new Color(playerColor.r, playerColor.g, playerColor.b, 0.75f);
    }
}
