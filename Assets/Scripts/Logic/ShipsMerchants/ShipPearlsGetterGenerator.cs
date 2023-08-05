using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipPearlsGetterGenerator: IGameObjectCreator
{
    GameObject shipPrefab;
    public event Action<ShipPearlsGetter> OnCreatedMerchant;
    public event Action<GameObject> OnCreatedInMapGameObject;

    MatchSO matchData;

    public ShipPearlsGetterGenerator(GameObject shipPrefab, PositionGenerator positionAsigner, PearlsPointsCalculator pearlsPointsCalculator, MatchSO matchData)
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
        var shipScript = GameObject.Instantiate(shipPrefab, Vector3.zero, Quaternion.identity).GetComponent<ShipPearlsGetter>();
        shipScript.Initialize(matchData.timeToGenerateMerchants);
        shipScript.OnDestroy += GenerateShipAfterDestroy;
        OnCreatedInMapGameObject?.Invoke(shipScript.gameObject);
        OnCreatedMerchant?.Invoke(shipScript);
    }

    void GenerateShipAfterDestroy(ShipPearlsGetter getter)
    {
        GenerateShip();
    }
}

