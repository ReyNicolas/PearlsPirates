using System;
using System.Collections;
using UnityEngine;

public class PearGenerator
{
    GameObject pearlPrefab;
    PositionGenerator positionAsigner;
    PowerGenerator powerGenerator;
    public event Action<PearlToObtain> OnCreatedPearlToObtain;
    public PearGenerator(GameObject pearlPrefab,  PositionGenerator positionAsigner, PowerGenerator powerGenerator)
    {
        this.pearlPrefab = pearlPrefab;
        this.positionAsigner = positionAsigner;
        this.powerGenerator = powerGenerator;        
    }

    public void CreatePearl()
    {
        var pearlToObtainGenerated = GameObject.Instantiate(pearlPrefab, positionAsigner.ReturnPosition(), Quaternion.identity).GetComponent<PearlToObtain>();
        powerGenerator.SetPearlPower(pearlToObtainGenerated);
        OnCreatedPearlToObtain?.Invoke(pearlToObtainGenerated);
    }        
    
}
