using System;
using System.Collections;
using UnityEngine;

public class PearlToObtain : MonoBehaviour
{
    [SerializeField] GameObject pearlSelectionPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool havePearl = true;
    [SerializeField] Power power;
    public void Initialize(Color color, Power power)
    {
       spriteRenderer.color = color;
       this.power = power;
    }
    public bool HavePearl()=> havePearl;

    public SelectionPearl GiveMeYourPearl()
    {
        havePearl= false;
        StartCoroutine(DestroyMe());      
        return GenerateSelectionPearl();
    }

    private SelectionPearl GenerateSelectionPearl()
    {
        SelectionPearl selectionPearl = Instantiate(pearlSelectionPrefab, transform.position, transform.rotation).GetComponent<SelectionPearl>();
        selectionPearl.Initialize(spriteRenderer.color, power);
        return selectionPearl;
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


}
