using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketsManager : MonoBehaviour
{
    ColorGenerator colorGenerator;
    [SerializeField] MatchSO matchData;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<MarketPier> marketPiers = new List<MarketPier>();
    [SerializeField] List<MarketShip> marketsShips = new List<MarketShip>();

    private void Awake()
    {
        MarketShip.onNewMarketShip += AddMerchant;
        MarketShip.onDestroy += RemoveMerchant;
        MarketPier.OnCollected += SetMarketPierColors;
    }


    private void Start()
    {
        colorGenerator = new ColorGenerator(PowersColors());
        marketPiers.ForEach(mp => { SetMarketPierColors(mp); });
    }

    private void OnDestroy()
    {
        MarketShip.onNewMarketShip -= AddMerchant;
        MarketShip.onDestroy -= RemoveMerchant;
        MarketPier.OnCollected -= SetMarketPierColors;
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
