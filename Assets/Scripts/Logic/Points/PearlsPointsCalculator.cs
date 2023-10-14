using System;

public class PearlsPointsCalculator
{
    public PearlsPointsCalculator(PlayerPointsGiver playerPointsGiver)
    {
        IMarket.OnSelectionPearlCollected += (pearlCollectedData=> AddPearlToPlayerPoints(pearlCollectedData,playerPointsGiver));
    }

   public void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData, PlayerPointsGiver playerPointsGiver)
    {
         playerPointsGiver.GivePoints(pearlCollectedData.playerData.PlayerName, 1);
    }

}

