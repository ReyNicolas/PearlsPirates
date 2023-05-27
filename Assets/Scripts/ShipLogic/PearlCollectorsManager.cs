using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PearlCollectorsManager : MonoBehaviour
{
    [SerializeField] List<PearlCollector> collectors;
    public int PlayerID;

    public List<SelectionPearl> GiveSelectionPearlsFromCollectors()
    {
        return collectors.Where(c=>!c.IsEmpty()).Select(c=> c.GetPearl()).ToList();
    }

}