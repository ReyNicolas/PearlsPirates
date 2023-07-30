using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator
{
    List<PowerSO> powersSOs = new List<PowerSO>();

    public PowerGenerator(List<PowerSO> powersSOs)
    {
        this.powersSOs = powersSOs;
    }

    public void SetPearlPower(PearlToObtain pearlToObtain)
    {
        PowerSO powerData = GetRandomPower();
        pearlToObtain.Initialize(powerData);
    }

    PowerSO GetRandomPower() =>
        powersSOs[Random.Range(0, powersSOs.Count)];

}