using System.Collections.Generic;

public class PointsManager
{
    List<PlayerSO> playersDatas;
    PearlsPointsCalculator pearlsPointsCalculator;
    PlayerPointsGiver playerPointsGiver;
    public PointsManager(List<PlayerSO> playersDatas)
    {
        this.playersDatas = playersDatas;
        playerPointsGiver = new PlayerPointsGiver();
        playerPointsGiver.OnGivePlayerPoints += AddPointsToPlayer;
        pearlsPointsCalculator = new PearlsPointsCalculator(playerPointsGiver);

    }   
    void AddPointsToPlayer(string playerName, int points) =>
        playersDatas.Find(pd => pd.PlayerName == playerName).PointsToAdd.Value += points;
}

