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
    }

    private void Update()
    {
        shipGenerateTimer -= Time.deltaTime;

        if(shipGenerateTimer<0 
            && matchData.maxNumberOfMerchants> matchData.merchantsInScene)
        {
            GenerateMarketShip();
        }
    }


    

    void GenerateMarketShip()
    {
        var marketShipScript = Instantiate(marketShipPrefab, Vector3.zero, Quaternion.identity).GetComponent<MarketShip>();
        marketShipScript.Initialize(matchData.timeToGenerateMerchants);
        marketShipScript.transform.position = positionGenerator.ReturnABorderPosition();
    }

}

