using System.Collections.Generic;

public class PointsManager
{
    List<PlayerSO> playersDatas;
    List<IPlayerPointsGiver> pointsGivers;

    public PointsManager(List<PlayerSO> playersDatas, List<IPlayerPointsGiver> pointsGivers)
    {
        this.playersDatas = playersDatas;
        this.pointsGivers = pointsGivers;
        this.pointsGivers.ForEach(pg => pg.OnGivePlayerPoints += AddPointsToPlayer);
    }   
    void AddPointsToPlayer(string playerName, int points) =>
        playersDatas.Find(pd => pd.PlayerName == playerName).PointsToAdd += points;
}

