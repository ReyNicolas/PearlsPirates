using System;
using System.Collections.Generic;

public class PearlsPointsCalculator: IPlayerPointsGiver
{
    public event Action<string, int> OnGivePlayerPoints;
    List<IMarket> markets = new List<IMarket>();

   public void  SubscribeToShipGenerator(MarketShipGenerator shipPearlsGetterGenerator) {

        shipPearlsGetterGenerator.OnCreatedMerchant += AddMarket;
    }

    void AddMarket(IMarket market)
    {
        markets.Add(market);
        market.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }

   public void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData) =>
        OnGivePlayerPoints?.Invoke(pearlCollectedData.playerData.PlayerName, 1);

}

