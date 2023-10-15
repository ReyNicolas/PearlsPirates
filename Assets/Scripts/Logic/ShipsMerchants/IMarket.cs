using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMarket: MonoBehaviour
{
    public static event Action<PearlCollectedDTO> OnSelectionPearlCollected;
    public static event Action<IMarket> onNewMarket;
    public static event Action<IMarket> onDestroyMarket;
    private void OnDestroy()
    {
        onDestroyMarket?.Invoke(this);
    }
    protected virtual void Start()
    {
        onNewMarket?.Invoke(this);
    }

  
    public abstract void TryToCollectThisPearlsFromThisPlayerData(List<SelectionPearl> selectionPearls, PlayerSO playerData);
    public abstract int GetNumberOfColors();
    public  abstract void SetColorsToCollect(List<Color> colors);

    protected void InvokeOnPearlColleted(PearlCollectedDTO pearlCollectedData) 
        => OnSelectionPearlCollected?.Invoke(pearlCollectedData);

    protected  PearlCollectedDTO GeneratePearlCollected(SelectionPearl pearl, PlayerSO playerData)
    {
        playerData.pearlsCollectedDatas.Add(pearl.GetPowerData());
        return new PearlCollectedDTO(pearl.GetPowerData(), playerData);
    }
}
