using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketsManager : MonoBehaviour
{
    ColorGenerator colorGenerator;
    MarketShipGenerator marketShipGenerator;
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject shipPrefab;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<MarketShip> marketsShips = new List<MarketShip>();
    [SerializeField] List<MarketPier> marketPiers = new List<MarketPier>();

    private void Awake()
    {
        MarketShip.onNewMarketShip += AddMerchant;
        MarketShip.OnDestroy += RemoveMerchant;
        
    }


    private void Start()
    {
        marketShipGenerator = new MarketShipGenerator(shipPrefab, gameManager.positionGenerator, matchData);
        colorGenerator = new ColorGenerator(PowersColors());
        
        marketShipGenerator.StartGeneration();
        marketPiers.ForEach(mp => { InitializeMarketPier(mp); });
    }

    void InitializeMarketPier(MarketPier mp)
    {
        mp.OnCollected += SetMarketPierColors;
        mp.OnSelectionPearlCollected += gameManager.pearlsPointsCalculator.AddPearlToPlayerPoints;
        SetMarketPierColors(mp);
    }

    void SetMarketPierColors(MarketPier pier)
    {
        if (pier.numberOfCollorsToCollect < 5) pier.numberOfCollorsToCollect++;
        colorGenerator.AddColorsToMarket(pier);
    }

    List<Color> PowersColors() => 
        matchData.powersDatas.Select(pd => pd.PowerColor).ToList();




    void AddMerchant(MarketShip merchantToAdd)
    {
        marketsShips.Add(merchantToAdd);
        matchData.merchantsInScene = marketsShips.Count;
    }

    void RemoveMerchant(MarketShip merchantToRemove)
    {
        marketsShips.Remove(merchantToRemove);
        matchData.merchantsInScene = marketsShips.Count;
    }


}
