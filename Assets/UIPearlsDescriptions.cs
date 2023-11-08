using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPearlsDescriptions : MonoBehaviour
{
    [SerializeField] UIPowerContainer containerPrefab;
    [SerializeField] MatchSO matchData;
    [SerializeField] Transform contentTransform;


    private void OnEnable()
    {
        foreach(PowerSO powerData in matchData.powersDatas)
        {
            Instantiate(containerPrefab, contentTransform)
                .Initialize(powerData);
        }
        
    }

    private void OnDisable()
    {
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
