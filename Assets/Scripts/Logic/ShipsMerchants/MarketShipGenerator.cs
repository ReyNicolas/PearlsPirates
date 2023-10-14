using System;
using UnityEngine;

public class MarketShipGenerator: IGameObjectCreator
{
    GameObject shipPrefab;
    public event Action<GameObject> onCreatedInMapGameObject;

    MatchSO matchData;

    public MarketShipGenerator(GameObject shipPrefab, PositionGenerator positionAsigner, MatchSO matchData)
    {
        this.shipPrefab = shipPrefab;       
        this.matchData = matchData;
        positionAsigner.AddObjectToListen(this);
        MarketShip.onNewMarketShip += GenerateShipAfterDestroy;
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
        onCreatedInMapGameObject?.Invoke(shipScript.gameObject);
    }

    public void GenerateShipAfterDestroy(MarketShip getter)
    {
        GenerateShip();
    }
}

