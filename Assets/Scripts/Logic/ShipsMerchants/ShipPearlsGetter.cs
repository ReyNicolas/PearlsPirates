using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipPearlsGetter : MonoBehaviour
{

    [SerializeField] List<Color> colorsToCollect = new List<Color>();
    [SerializeField] List<Transform> pearlsContainers;
    public Action<PearlCollectedDTO> OnSelectionPearlCollected;
    public event Action<ShipPearlsGetter> OnDestroy;
    public event Action<List<Color>> OnChangeColors;
    public void Initialize(float timeAlive)
    {
        StartCoroutine(DestroyMe(timeAlive));
    }
     
    IEnumerator DestroyMe(float timeAlive)
    {
        yield return new WaitForSeconds(timeAlive);
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }

    public int GetNumberOfContainers()
    {
        return pearlsContainers.Count;
    }
    public void SetColorsToCollect(List<Color> colors)
    {
        colorsToCollect = colors;
        OnChangeColors?.Invoke(colorsToCollect);
    }


    public bool TryToCollectThisPearlFromThisPlayer (SelectionPearl pearl, PlayerSO playerData)
    {        
        if (NeedsToCollectThisColor(pearl.GetColor()))
        {
            CollectThisPearl(pearl,playerData);
            return true;
        }
        return false;        
    }

    bool NeedsToCollectThisColor(Color color) =>
        colorsToCollect.Contains(color);

    void CollectThisPearl(SelectionPearl pearl, PlayerSO playerData)
    {
        colorsToCollect.Remove(pearl.GetColor());
        OnChangeColors?.Invoke(colorsToCollect);
        OnSelectionPearlCollected?.Invoke(GeneratePearlCollected(pearl, playerData));
        SetConainerToPearl(pearl);
        if(colorsToCollect.Count == 0) StartCoroutine(DestroyMe(0.1f));
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
