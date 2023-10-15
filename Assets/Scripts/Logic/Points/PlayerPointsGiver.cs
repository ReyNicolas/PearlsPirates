using System;

public static class PlayerPointsGiver
{
   public static event Action<string, int> OnGivePlayerPoints; 

    public static void GivePoints(string playerName, int points)
    {
        OnGivePlayerPoints?.Invoke(playerName, points);
    }
}

