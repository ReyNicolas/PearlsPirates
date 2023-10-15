using System.Collections.Generic;
using UnityEngine;

public class PointsManager: MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    List<PlayerSO> playersDatas;

    private void Start()
    {
        playersDatas = matchData.playersDatas;

        PlayerPointsGiver.OnGivePlayerPoints += AddPointsToPlayer;
        IMarket.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }
    private void OnDestroy()
    {
        PlayerPointsGiver.OnGivePlayerPoints -= AddPointsToPlayer;
        IMarket.OnSelectionPearlCollected -= AddPearlToPlayerPoints;
    }

    void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData)
    {
        PearlsPointsCalculator.AddPearlToPlayerPoints(pearlCollectedData);
    }

    void AddPointsToPlayer(string playerName, int points) =>
        playersDatas.Find(pd => pd.PlayerName == playerName).PointsToAdd.Value += points;
}
