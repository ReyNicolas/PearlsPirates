using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketShip : IMarket
{

    [SerializeField] List<Color> colorsToCollect = new List<Color>();
    [SerializeField] List<Transform> pearlsContainers;
    
    public static event Action<MarketShip> onNewMarketShip;
    public static event Action<MarketShip> onDestroy;
    public event Action<List<Color>> OnChangeColors;


    protected override void Start()
    {
        base.Start();
        onNewMarketShip?.Invoke(this);
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
    }


    public override int GetNumberOfColors()
    {
        return pearlsContainers.Count;
    }
    public override void SetColorsToCollect(List<Color> colors)
    {
        colorsToCollect = colors;
        OnChangeColors?.Invoke(colorsToCollect);
    }


    public override void TryToCollectThisPearlsFromThisPlayerData(List<SelectionPearl> selectionPearls, PlayerSO playerData)
    {
        List<SelectionPearl> pearlsToSelect = selectionPearls;
        List<SelectionPearl> pearlsToCollect = new List<SelectionPearl>();
        SelectionPearl pearl ;

        for(int i = 0; i < colorsToCollect.Count; i++)
        {
            pearl = pearlsToSelect.Find(ps => ps.GetPowerData().PowerColor == colorsToCollect[i]);
            if (pearl != null) 
            {
                pearlsToCollect.Add(pearl);
                pearlsToSelect.Remove(pearl);
            }
        }
        if (pearlsToCollect.Count == colorsToCollect.Count) CollectPearls(pearlsToCollect, playerData);

    }

    void CollectPearls(List<SelectionPearl> pearlsSelected, PlayerSO playerData)
    {
        pearlsSelected.ForEach(ps => CollectThisPearl(ps, playerData));
        colorsToCollect.Clear();
        OnChangeColors?.Invoke(colorsToCollect);
        Destroy(gameObject);
    }

    void CollectThisPearl(SelectionPearl pearl, PlayerSO playerData)
    {
        InvokeOnPearlColleted(GeneratePearlCollected(pearl, playerData));
        SetConainerToPearl(pearl);
    }

    void SetConainerToPearl(SelectionPearl pearl)
    {
        pearl.transform.position = pearlsContainers.First().position;
        pearl.transform.SetParent(pearlsContainers.First());
        pearlsContainers.RemoveAt(0);
    }

   

}
