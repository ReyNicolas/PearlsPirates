using System.Collections.Generic;

public class PointsContainer
{
    Dictionary<int, int> playersIDsToPoints = new Dictionary<int, int>();
    List<IPlayerPointsGiver> pointsGivers = new List<IPlayerPointsGiver>();

    public void AddPointsGiver(IPlayerPointsGiver pointsGiver)
    {
        pointsGiver.OnGivePlayerPoints += AddPointsToPlayer;
        pointsGivers.Add(pointsGiver);
    }

    public void AddPlayersIDS(List<int> playersIDs) => 
        playersIDs.ForEach(pids => playersIDsToPoints.Add(pids, 0));

    void AddPointsToPlayer(int playerID, int points) =>
        playersIDsToPoints[playerID] += points;  
}

