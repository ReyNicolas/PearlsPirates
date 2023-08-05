using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchData", menuName = "Match Data")]
public class MatchSO : ScriptableObject
{
    [Header("Data")]
    public List<PlayerSO> playersDatas;
    public List<PowerSO> powersDatas;
    public GameObject pearlPrefab;
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

    public void Initialize()
    {        
        numberPearlsToObtainInScene = 0;
    }


}

