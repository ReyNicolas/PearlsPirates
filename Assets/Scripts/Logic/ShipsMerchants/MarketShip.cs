using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketShip : MonoBehaviour, IMarket
{

    [SerializeField] List<Color> colorsToCollect = new List<Color>();
    [SerializeField] List<Transform> pearlsContainers;
    public event Action<PearlCollectedDTO> OnSelectionPearlCollected;
    public event Action<MarketShip> OnDestroy;
    public event Action<List<Color>> OnChangeColors;
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
    public int GetNumberOfColors()
    {
        return pearlsContainers.Count;
    }
    public void SetColorsToCollect(List<Color> colors)
    {
        colorsToCollect = colors;
        OnChangeColors?.Invoke(colorsToCollect);
    }


    public void TryToCollectThisPearlsFromThisPlayerData(List<SelectionPearl> selectionPearls, PlayerSO playerData)
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
        OnSelectionPearlCollected?.Invoke(GeneratePearlCollected(pearl, playerData));
        SetConainerToPearl(pearl);
    }

    void SetConainerToPearl(SelectionPearl pearl)
    {
        pearl.transform.position = pearlsContainers.First().position;
        pearl.transform.SetParent(pearlsContainers.First());
        pearlsContainers.RemoveAt(0);
    }

    PearlCollectedDTO GeneratePearlCollected(SelectionPearl pearl, PlayerSO playerData)
    {
        playerData.pearlsCollectedDatas.Add(pearl.GetPowerData());
        return new PearlCollectedDTO(pearl.GetPowerData(), playerData); 
    }

}
public interface IMarket
{
    void TryToCollectThisPearlsFromThisPlayerData(List<SelectionPearl> selectionPearls, PlayerSO playerData);
    event Action<PearlCollectedDTO> OnSelectionPearlCollected;
    int GetNumberOfColors();
    void SetColorsToCollect(List<Color> colors);
}
