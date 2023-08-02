using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] GameManager matchManager;
    PearGenerator pearGenerator;
    PowerGenerator powerGenerator;
    PearlsInMatchController pearlsInMatchController;

    private void Awake()
    {
        powerGenerator = new PowerGenerator(matchData.powersDatas);
        pearGenerator = new PearGenerator(matchData.pearlPrefab, matchManager.positionGenerator, powerGenerator);
        pearlsInMatchController = new PearlsInMatchController(matchData, pearGenerator);
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
            if(matchData.numberPearlsToObtainInScene< matchData.maxNumberOfPearls) pearGenerator.CreatePearl();
            yield return new WaitForSeconds(matchData.timeToGeneratePearl);
        }
        
    }
}


   
