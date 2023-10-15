using System;
using UnityEngine;

public class MarketShipGenerator: MonoBehaviour
{
    [SerializeField] GameObject marketShipPrefab;
    [SerializeField] MatchSO matchData;
    [SerializeField] GameManager gameManager;
    PositionGenerator positionGenerator;
    float shipGenerateTimer;

    private void Start()
    {
        positionGenerator = gameManager.positionGenerator;
        shipGenerateTimer = matchData.timeToGenerateMerchants;
    }

    private void Update()
    {
        shipGenerateTimer -= Time.deltaTime;

        if(shipGenerateTimer<0 
            && matchData.maxNumberOfMerchants> matchData.merchantsInScene)
        {
            GenerateMarketShip();
            shipGenerateTimer = matchData.timeToGenerateMerchants;
        }
    }


    

    void GenerateMarketShip()
    {
        var borderPositionOrigin = positionGenerator.ReturnABorderPosition();
        var moveToPositionLogic = Instantiate(marketShipPrefab,borderPositionOrigin , Quaternion.identity).GetComponent<MoveToPositionLogic>();
        moveToPositionLogic.SetPositionToMove(positionGenerator.ReturnABorderPositionToMove(borderPositionOrigin));
    }

}

