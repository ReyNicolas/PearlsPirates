using System;
using System.Collections;
using UnityEngine;

public class PearlToObtain : MonoBehaviour
{
    [SerializeField] GameObject pearlSelectionPrefab;
    [SerializeField] bool havePearl = true;
    [SerializeField] PowerSO powerData;
    public void Initialize(PowerSO powerData)
    {
        this.powerData = powerData;
       GetComponent<SpriteRenderer>().color = powerData.PowerColor;
    }
    public bool HavePearl()=> 
        havePearl;

    public SelectionPearl GiveMeYourPearl()
    {
        havePearl= false;
        StartCoroutine(DestroyMe());      
        return GenerateSelectionPearl();
    }

    private SelectionPearl GenerateSelectionPearl()
    {
        SelectionPearl selectionPearl = Instantiate(pearlSelectionPrefab, transform.position, transform.rotation).GetComponent<SelectionPearl>();
        selectionPearl.Initialize(powerData);
        return selectionPearl;
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


}
