using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PearlManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] GameManager matchManager;
    PearlGenerator pearlGenerator;
    PearlPowerGenerator powerGenerator;
    PearlsInMatchController pearlsInMatchController;

    private void Awake()
    {
        pearlGenerator = new PearlGenerator(matchData.pearlPrefab);
        powerGenerator = new PearlPowerGenerator(matchData.powersDatas,pearlGenerator);
        pearlGenerator.onCreatedForMapGameObject += SetPosition;
        pearlsInMatchController = new PearlsInMatchController(matchData, pearlGenerator);
    }

   

    private void Start()
    {
        StartCoroutine(GeneratePearl());
        StartCoroutine(PlayWind());
    }

    private void OnDestroy()
    {
        pearlGenerator.onCreatedForMapGameObject -= SetPosition;
    }

    void SetPosition(GameObject go)
    {
        matchManager.positionGenerator.AssignPosition(go);
    }

    IEnumerator PlayWind()
    {
        while (true)
        {
            pearlsInMatchController.pearlsToObtains.ForEach(pearl => pearl.AddForce(matchData.wind));
            yield return new WaitForSeconds(1f);
            pearlsInMatchController.pearlsToObtains.ForEach(pearl => pearl.AddForce(-matchData.wind));
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator GeneratePearl()
    {
        while (true)
        {
            if(matchData.numberPearlsToObtainInScene< matchData.maxNumberOfPearls) 
                pearlGenerator.CreatePearl();
            yield return new WaitForSeconds(matchData.timeToGeneratePearl);
        }
        
    }


}


   
