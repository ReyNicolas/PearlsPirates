using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{
    ColorGenerator colorGenerator;
    ShipPearlsGetterGenerator shipGenerator;
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject shipPrefab;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<ShipPearlsGetter> merchants = new List<ShipPearlsGetter>();

  
    private void Start()
    {
        shipGenerator = new ShipPearlsGetterGenerator(shipPrefab, gameManager.positionGenerator, gameManager.pearlsPointsCalculator, matchData);
        colorGenerator = new ColorGenerator(PowersColors(), shipGenerator);
        shipGenerator.OnCreatedMerchant += AddMerchant;
        shipGenerator.StartGeneration();
    }

    List<Color> PowersColors() => 
        matchData.powersDatas.Select(pd => pd.PowerColor).ToList();

    void AddMerchant(ShipPearlsGetter merchantToAdd)
    {
        merchants.Add(merchantToAdd);

        matchData.merchantsInScene = merchants.Count;
        merchantToAdd.OnDestroy += RemoveMerchant;
    }

    void RemoveMerchant(ShipPearlsGetter merchantToRemove)
    {
        merchants.Remove(merchantToRemove);
        matchData.merchantsInScene = merchants.Count;
    }


}
