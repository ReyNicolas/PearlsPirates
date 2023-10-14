using System;

public class PlayerPointsGiver
{
   public  event Action<string, int> OnGivePlayerPoints; 

    public  void GivePoints(string playerName, int points)
    {
        OnGivePlayerPoints?.Invoke(playerName, points);
    }
}

