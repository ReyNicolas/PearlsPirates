using System;
using System.Collections.Generic;

public class PearlsPointsCalculator: IPlayerPointsGiver
{
    public event Action<string, int> OnGivePlayerPoints;
    List<ShipPearlsGetter> shipsPearlsGetters = new List<ShipPearlsGetter>();

   public void  SubscribeToShipGenerator(ShipPearlsGetterGenerator shipPearlsGetterGenerator) {

        shipPearlsGetterGenerator.OnCreatedMerchant += AddShipPearlsGetter;
    }

    void AddShipPearlsGetter(ShipPearlsGetter ship)
    {
        shipsPearlsGetters.Add(ship);
        ship.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }

    void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData) =>
        OnGivePlayerPoints?.Invoke(pearlCollectedData.playerData.PlayerName, 1);

}

