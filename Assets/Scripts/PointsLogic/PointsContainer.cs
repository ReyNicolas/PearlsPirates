using System.Collections.Generic;

public class PointsContainer
{
    List<ShipPearlsGetter> shipsPearlsGetters = new List<ShipPearlsGetter>();
    Dictionary<int, int> playersIDsToPoints = new Dictionary<int, int>();

    public void AddPlayersIDS(List<int> playersIDs) => playersIDs.ForEach(pids => playersIDsToPoints.Add(pids, 0));

    public void AddShipPearlsGetter(ShipPearlsGetter ship)
    {        
        shipsPearlsGetters.Add(ship);
        ship.OnSelectionPearlCollected += AddPearlToPlayerPoints;
    }

    void AddPearlToPlayerPoints(PearlCollectedDTO pearlCollectedData)=> 
        playersIDsToPoints.Add(pearlCollectedData.playerID, GetPearlFinalPoints(pearlCollectedData));
  
    int GetPearlFinalPoints(PearlCollectedDTO pearlCollectedData) => 
        playersIDsToPoints[pearlCollectedData.playerID] + SetBonusToPoints(pearlCollectedData.bonusID, GetPearlPoints(pearlCollectedData.pearl));

    int SetBonusToPoints(string bonus, int points) => points;

    int GetPearlPoints(SelectionPearl selectionPearl) => 1;    
    
}
