using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PearlCollectorsManager : MonoBehaviour
{
    [SerializeField] List<PearlCollector> collectors;
    public PlayerSO playerData;

    private void Start()
    {
        collectors.ForEach(c => c.playerData = playerData);
    }

}
