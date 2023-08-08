using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "MatchData", menuName = "Match Data")]
public class MatchSO : ScriptableObject
{
    [Header("Data")]
    public List<PlayerSO> playersDatas;
    public List<PowerSO> powersDatas;
    public GameObject playerShipPrefab;
    public GameObject pearlPrefab;
    public GameObject instantPearlPrefab;
    [Header("End conditions Settings")]
    public int totalPointsLimit;
    public int totalPlayerPointsLimit;
    public float timeLeft;
    [Header("Pearls Settings")]
    public int numberPearlsToObtainInScene;
    public int timeToGeneratePearl;
    public int maxNumberOfPearls;
    [Header("Merchants Settings")]
    public int merchantsInScene;
    public int timeToGenerateMerchants;
    public int maxNumberOfMerchants;
    [Header("Wind Settings")]    
    public Vector2 wind;

    public ReactiveProperty<PlayerSO> winnerData = new ReactiveProperty<PlayerSO>(null);

    public void Initialize()
    {        
        numberPearlsToObtainInScene = 0;
        playersDatas.ForEach(pd => pd.PointsToAdd.Subscribe(value => CheckWinner(value)));
        winnerData.Value = null;
    }

    void CheckWinner(int value)
    {
        if (EndedByTotalPointsLimit() || EndedByTotalPlayerPointsLimit())
        {
            SetWinner();
        }
    }

    void SetWinner()
    {
        winnerData.Value = playersDatas.OrderByDescending(pd => pd.PointsToAdd.Value).First();
    }

    bool EndedByTotalPlayerPointsLimit()
    {
        return playersDatas.Any(pd => pd.PointsToAdd.Value >= totalPlayerPointsLimit);
    }

    bool EndedByTotalPointsLimit()
    {
        return (playersDatas.Sum(pd => pd.PointsToAdd.Value)) >= totalPointsLimit;
    }
}

