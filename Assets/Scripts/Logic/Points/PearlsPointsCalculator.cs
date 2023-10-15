using System;

public class PearlsPointsCalculator
{
    PlayerPointsGiver playerPointsGiver;
    public PearlsPointsCalculator(PlayerPointsGiver playerPointsGiver)
    {
        this.playerPointsGiver = playerPointsGiver;
    }

    public void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData)
    {
         playerPointsGiver.GivePoints(pearlCollectedData.playerData.PlayerName, 1);
    }

}

