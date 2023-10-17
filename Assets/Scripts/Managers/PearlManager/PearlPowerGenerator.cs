using System.Collections.Generic;
using UnityEngine;

public class PearlPowerGenerator
{
    List<PowerSO> powersSOs = new List<PowerSO>();

    public PearlPowerGenerator(List<PowerSO> powersSOs, PearlGenerator pearlGenerator )
    {
        this.powersSOs = powersSOs;
        pearlGenerator.OnCreatedPearlToObtain += SetPearlPower;
    }

    void SetPearlPower(PearlToObtain pearlToObtain)
    {
        PowerSO powerData = GetRandomPower();
        pearlToObtain.Initialize(powerData);
    }

    PowerSO GetRandomPower() =>
        powersSOs[Random.Range(0, powersSOs.Count)];

}