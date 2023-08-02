using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchData", menuName = "Match Data")]
public class MatchSO : ScriptableObject
{
     public List<PlayerSO> playersDatas;
    public List<PowerSO> powersDatas;
    public GameObject pearlPrefab;
    public int totalPointsLimit;
    public int totalPlayerPointsLimit;
    public int numberPearlsToObtainInScene;
    public int timeToGeneratePearl;
    public int maxNumberOfPearls;
    public float timeLeft;
    public Vector2 wind;

    public void Initialize()
    {        
        numberPearlsToObtainInScene = 0;
    }


}

