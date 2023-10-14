using System;
using System.Collections.Generic;
using UnityEngine;

public class MarketPier: IMarket
{
    [SerializeField] List<Color> colorsToCollect = new List<Color>();
    public int numberOfCollorsToCollect;
    public event Action<List<Color>> OnChangeColors;
    public event Action<MarketPier> OnCollected;

    public override int GetNumberOfColors()
    {
        return numberOfCollorsToCollect;
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

        for (int i = 0; i < colorsToCollect.Count; i++)
        {
            var pearl = pearlsToSelect.Find(ps => ps.GetPowerData().PowerColor == colorsToCollect[i]);
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
        OnCollected?.Invoke(this);
    }

    void CollectThisPearl(SelectionPearl pearl, PlayerSO playerData)
    {
        InvokeOnPearlColleted(GeneratePearlCollected(pearl, playerData));
        Destroy(pearl.gameObject);
    }
    

}
