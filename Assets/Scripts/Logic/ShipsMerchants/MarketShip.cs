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
    public static event Action<MarketShip> OnDestroy;
    public event Action<List<Color>> OnChangeColors;

    private void Start()
    {
        onNewMarketShip?.Invoke(this);
    }
    public void Initialize(float timeAlive)
    {
        StopCoroutine("DestroyMe");
        StartCoroutine(DestroyMe(timeAlive));
    }
     
    IEnumerator DestroyMe(float timeAlive)
    {
        yield return new WaitForSeconds(timeAlive);
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
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
        
        for(int i = 0; i < colorsToCollect.Count; i++)
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
        StopCoroutine("DestroyMe");
        StartCoroutine(DestroyMe(0.1f));
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
public abstract class IMarket: MonoBehaviour
{
   public static event  Action<PearlCollectedDTO> OnSelectionPearlCollected;
    public  static event Action<IMarket> onNewMarket;
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
