using System.Collections.Generic;
using UnityEngine;

public class PointsManager: MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    List<PlayerSO> playersDatas;
    PearlsPointsCalculator pearlsPointsCalculator;
    PlayerPointsGiver playerPointsGiver;

    private void Start()
    {
        playersDatas = matchData.playersDatas;

        playerPointsGiver = new PlayerPointsGiver();
        playerPointsGiver.OnGivePlayerPoints += AddPointsToPlayer;

        pearlsPointsCalculator = new PearlsPointsCalculator(playerPointsGiver);
        IMarket.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }
    private void OnDestroy()
    {
        playerPointsGiver.OnGivePlayerPoints -= AddPointsToPlayer;
        IMarket.OnSelectionPearlCollected -= AddPearlToPlayerPoints;
    }

    void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData)
    {
        pearlsPointsCalculator.AddPearlToPlayerPoints(pearlCollectedData);
    }

    void AddPointsToPlayer(string playerName, int points) =>
        playersDatas.Find(pd => pd.PlayerName == playerName).PointsToAdd.Value += points;
}

