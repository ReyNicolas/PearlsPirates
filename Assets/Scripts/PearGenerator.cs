using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearGenerator : MonoBehaviour
{
    [SerializeField]GameObject pearlPrefab;
    [SerializeField]List<Power> powers;

    void Start()//prueba
    {
        for(int i = 0; i < 10; i++)
        {
            CreatePearl();
        }
    }

    void CreatePearl()
    {
        SetPearlPower(GeneratePearlGO().GetComponent<PearlToObtain>());
    }

    GameObject GeneratePearlGO() => Instantiate(pearlPrefab, GetRandomPosition(), Quaternion.identity);

    Vector3 GetRandomPosition()=> new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f),0);    

    void SetPearlPower(PearlToObtain pearlToObtain)
    {
        Power power = GetRandomPower();
        pearlToObtain.Initialize(power.ColorToPearl, power);
    }

    Power GetRandomPower()=> Instantiate(powers[Random.Range(0, powers.Count)],transform.position,transform.rotation);
  
}
