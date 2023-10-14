using System;
using System.Collections.Generic;

public class PearlsPointsCalculator: IPlayerPointsGiver
{
    public event Action<string, int> OnGivePlayerPoints;


    public PearlsPointsCalculator()
    {
        IMarket.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }

   public void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData) =>
        OnGivePlayerPoints?.Invoke(pearlCollectedData.playerData.PlayerName, 1);

}

