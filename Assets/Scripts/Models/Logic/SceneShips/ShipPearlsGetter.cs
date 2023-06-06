using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPearlsGetter : MonoBehaviour
{
    [SerializeField] List<Color> colorsToCollect= new List<Color>();
    public Action<PearlCollectedDTO> OnSelectionPearlCollected;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PearlCollectorsManager>())
        {
            PearlCollectorsManager collectorsManager= collision.GetComponent<PearlCollectorsManager>();
            TryToCollectThisPearls(collectorsManager.GiveSelectionPearlsFromCollectors(), collectorsManager);
        }
    }

    public void AddColorsToCollect(List<Color> colors) => 
        colorsToCollect.AddRange(colors);


    void TryToCollectThisPearls(List<SelectionPearl> pearls, PearlCollectorsManager collectorsManager)
    {
        foreach(SelectionPearl pearl in pearls)
        {
            if (NeedsToCollectThisColor(pearl.GetColor()))
            {
                CollectThisPearl(pearl, collectorsManager);
            }
        }
    }

    void CollectThisPearl(SelectionPearl pearl, PearlCollectorsManager collectorsManager)
    {
        colorsToCollect.Remove(pearl.GetColor());
        OnSelectionPearlCollected?.Invoke( GeneratePearlCollected(pearl,collectorsManager) );
        Destroy(pearl.gameObject);
    }

    bool NeedsToCollectThisColor(Color color)=> 
        colorsToCollect.Contains(color);
    
    PearlCollectedDTO GeneratePearlCollected(SelectionPearl pearl, PearlCollectorsManager collectorsManager) =>
        new PearlCollectedDTO(pearl, collectorsManager.PlayerID, "None"); 
  
}
