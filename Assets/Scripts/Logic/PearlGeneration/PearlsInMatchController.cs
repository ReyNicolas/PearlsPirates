using System.Collections.Generic;

public class PearlsInMatchController
{
    public List<PearlToObtain> pearlsToObtains = new List<PearlToObtain>();
    MatchSO matchData;

    public PearlsInMatchController(MatchSO matchData, PearlGenerator pearGenerator)
    {
        this.matchData = matchData;
        pearGenerator.OnCreatedPearlToObtain += AddPearl;
    }

    void AddPearl(PearlToObtain pearlToAdd)
    {
        pearlsToObtains.Add(pearlToAdd);

        matchData.numberPearlsToObtainInScene = pearlsToObtains.Count;
        pearlToAdd.OnDestroy += RemovePearl;
    }

    void RemovePearl(PearlToObtain pearlToRemove)
    {
        pearlsToObtains.Remove(pearlToRemove);

        matchData.numberPearlsToObtainInScene = pearlsToObtains.Count;
    }
}
