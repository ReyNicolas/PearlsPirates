using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipGenerator
{
    GameObject shipPrefab;
    PositionAsigner positionAsigner;
    PearlsPointsCalculator pearlsPointsCalculator;
    ColorGenerator colorGenerator;
    List<ShipPearlsGetter> shipPearlsGetterList = new List<ShipPearlsGetter>();
    int numberOfColorsToCollectPerShip = 3;

    public  ShipGenerator(GameObject shipPrefab, PositionAsigner positionAsigner, PearlsPointsCalculator pearlsPointsCalculator,ColorGenerator colorGenerator)
    {
        this.shipPrefab = shipPrefab;
        this.positionAsigner = positionAsigner;
        GenerateShipPool();
        this.pearlsPointsCalculator = pearlsPointsCalculator;
        this.colorGenerator = colorGenerator;
    }

    public void ActiveShip()
    {
        var shipScript = GetShipScript();
        shipScript.gameObject.SetActive(true);
        shipScript.transform.position = positionAsigner.ReturnPosition();
    }

    void GenerateShipPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject ship;
            ShipPearlsGetter shipPearlsGetter;
            GenerateShip(out ship, out shipPearlsGetter);
            AddShipToList(shipPearlsGetter);
            AddColorsToCollectToShip(shipPearlsGetter);
            AddShiptToPointcalculaor(shipPearlsGetter);
            ship.SetActive(false);
        }
    }

    void AddColorsToCollectToShip(ShipPearlsGetter shipPearlsGetter) =>
        shipPearlsGetter.AddColorsToCollect(colorGenerator.GetThisNumberOfRandomColors(numberOfColorsToCollectPerShip));

    void GenerateShip(out GameObject ship, out ShipPearlsGetter shipPearlsGetter)
    {
        ship = GameObject.Instantiate(shipPrefab, Vector2.zero, Quaternion.identity);
        shipPearlsGetter = ship.GetComponent<ShipPearlsGetter>();
    }

    void AddShiptToPointcalculaor(ShipPearlsGetter shipPearlsGetter) => 
        pearlsPointsCalculator.AddShipPearlsGetter(shipPearlsGetter);

    void AddShipToList(ShipPearlsGetter shipPearlsGetter) => 
        shipPearlsGetterList.Add(shipPearlsGetter);

    ShipPearlsGetter GetShipScript() =>
        AreThereInActiveShipScript() ? InActiveShipScript() : RandomShipScript();

    bool AreThereInActiveShipScript() =>
        shipPearlsGetterList.Any(sp => !sp.gameObject.activeSelf);        
    
    ShipPearlsGetter InActiveShipScript()=> 
        shipPearlsGetterList.Find(sp=>!sp.gameObject.activeSelf);

    ShipPearlsGetter RandomShipScript()=> 
        shipPearlsGetterList[Random.Range(0, shipPearlsGetterList.Count)];


}
