using System;
using System.Collections.Generic;

public class PearlsPointsCalculator: IPlayerPointsGiver
{
    public event Action<string, int> OnGivePlayerPoints;
    List<ShipPearlsGetter> shipsPearlsGetters = new List<ShipPearlsGetter>();

    public void AddShipPearlsGetter(ShipPearlsGetter ship)
    {
        shipsPearlsGetters.Add(ship);
        ship.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }

    void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData) =>
        OnGivePlayerPoints?.Invoke(pearlCollectedData.playerName, GetPearlFinalPoints(pearlCollectedData));

    int GetPearlFinalPoints(PearlCollectedDTO pearlCollectedData) =>
        SetBonusToPoints(pearlCollectedData.bonusID, GetPearlPoints(pearlCollectedData.pearl));

    int SetBonusToPoints(string bonus, int points) => 
        points;

    int GetPearlPoints(SelectionPearl selectionPearl) => 
        1;
}

