using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchData", menuName = "Match Data")]
public class MatchSO : ScriptableObject
{
   public List<PlayerSO> playersDatas;
    public int totalPointsLimit;
    public int totalPlayerPointsLimit;
    public int numberPearlsToObtainInScene;
    public float timeLeft;

    public void Initialize(List<PlayerSO> playersDatas, int totalPointsLimit, int totalPlayerPointsLimit, float timeLeft)
    {
        this.playersDatas = playersDatas;
        this.totalPointsLimit = totalPointsLimit;
        this.totalPlayerPointsLimit = totalPlayerPointsLimit;       
        this.timeLeft = timeLeft;
        numberPearlsToObtainInScene = 0;
    }
}

