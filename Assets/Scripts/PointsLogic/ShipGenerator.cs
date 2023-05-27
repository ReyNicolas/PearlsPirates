using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipGenerator
{
    GameObject shipPrefab;
    PositionAsigner positionAsigner;
    List<ShipPearlsGetter> shipPearlsGetterList = new List<ShipPearlsGetter>();

    public  ShipGenerator(GameObject shipPrefab, PositionAsigner positionAsigner)
    {
        this.shipPrefab = shipPrefab;
        this.positionAsigner = positionAsigner;
        GenerateShipPool();        
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
            var ship = GameObject.Instantiate(shipPrefab, Vector2.zero, Quaternion.identity);
            shipPearlsGetterList.Add(ship.GetComponent<ShipPearlsGetter>());
            ship.SetActive(false);
        }
    }
    ShipPearlsGetter GetShipScript() => AreThereInActiveShipScript() ? InActiveShipScript() : RandomShipScript();
    bool AreThereInActiveShipScript() =>shipPearlsGetterList.Any(sp => !sp.gameObject.activeSelf);         
    ShipPearlsGetter InActiveShipScript() => shipPearlsGetterList.Find(sp=>!sp.gameObject.activeSelf);
    ShipPearlsGetter RandomShipScript() => shipPearlsGetterList[Random.Range(0, shipPearlsGetterList.Count)];


}
