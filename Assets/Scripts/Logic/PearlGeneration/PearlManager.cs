using System.Collections;
using System.Collections.Generic;
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
        matchManager.positionGenerator.AddObjectToListen(pearlGenerator);
        pearlsInMatchController = new PearlsInMatchController(matchData, pearlGenerator);
    }

    private void Start()
    {
        StartCoroutine(GeneratePearl());
        StartCoroutine(PlayWind());
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
            if(matchData.numberPearlsToObtainInScene< matchData.maxNumberOfPearls) pearlGenerator.CreatePearl();
            yield return new WaitForSeconds(matchData.timeToGeneratePearl);
        }
        
    }
}


   
