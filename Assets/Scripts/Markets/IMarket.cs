using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IMarket: MonoBehaviour
{

    [SerializeField] protected List<Color> colorsToCollect = new List<Color>();
    public static List<IMarket> MarketList = new List<IMarket>();
    public static event Action<PearlCollectedDTO> OnSelectionPearlCollected;
    public static event Action<IMarket> onNewMarket;
    public static event Action<IMarket> onDestroyMarket;


    protected virtual void OnDestroy()
    {
        onDestroyMarket?.Invoke(this);
        MarketList.Remove(this);
    }
    protected virtual void Start()
    {
        MarketList.Add(this);
        onNewMarket?.Invoke(this);
    }

  
    public abstract void TryToCollectThisPearlsFromThisPlayerData(List<SelectionPearl> selectionPearls, PlayerSO playerData);
    public abstract int GetNumberOfColors();
    public  abstract void SetColorsToCollect(List<Color> colors);
    public bool HaveAllColorsToCollect(List<Color> colorsToCheck)
    {
        var groupToCheck = colorsToCheck.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        var group = colorsToCollect.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        foreach (var pair in group)
        {
            if (!groupToCheck.TryGetValue(pair.Key, out int countToCheck) || countToCheck < pair.Value)
            {
                return false;
            }
        }

        return true;
    }

    protected void InvokeOnPearlColleted(PearlCollectedDTO pearlCollectedData) 
        => OnSelectionPearlCollected?.Invoke(pearlCollectedData);

    protected  PearlCollectedDTO GeneratePearlCollected(SelectionPearl pearl, PlayerSO playerData)
    {
        playerData.pearlsCollectedDatas.Add(pearl.GetPowerData());
        return new PearlCollectedDTO(pearl.GetPowerData(), playerData);
    }
}
