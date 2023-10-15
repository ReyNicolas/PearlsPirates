using System;

public static class PearlsPointsCalculator
{
    
    public static void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData)
    {
         PlayerPointsGiver.GivePoints(pearlCollectedData.playerData.PlayerName, 1);
    }

}

