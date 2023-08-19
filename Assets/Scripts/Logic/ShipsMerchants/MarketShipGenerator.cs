using System;
using UnityEngine;

public class MarketShipGenerator: IGameObjectCreator
{
    GameObject shipPrefab;
    public event Action<MarketShip> OnCreatedMerchant;
    public event Action<GameObject> OnCreatedInMapGameObject;

    MatchSO matchData;

    public MarketShipGenerator(GameObject shipPrefab, PositionGenerator positionAsigner, PearlsPointsCalculator pearlsPointsCalculator, MatchSO matchData)
    {
        this.shipPrefab = shipPrefab;       
        this.matchData = matchData;
        pearlsPointsCalculator.SubscribeToShipGenerator(this);
        positionAsigner.AddObjectToListen(this);
    }

    public void StartGeneration()
    {
        for(int i=0; i<matchData.maxNumberOfMerchants; i++)
        {
            GenerateShip();
        }
    }

    void GenerateShip()
    {
        var shipScript = GameObject.Instantiate(shipPrefab, Vector3.zero, Quaternion.identity).GetComponent<MarketShip>();
        shipScript.Initialize(matchData.timeToGenerateMerchants);
        shipScript.OnDestroy += GenerateShipAfterDestroy;
        OnCreatedInMapGameObject?.Invoke(shipScript.gameObject);
        OnCreatedMerchant?.Invoke(shipScript);
    }

    void GenerateShipAfterDestroy(MarketShip getter)
    {
        GenerateShip();
    }
}

