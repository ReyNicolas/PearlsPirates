using System;
using System.Collections;
using UnityEngine;

public class PearlToObtain : MonoBehaviour
{
    [SerializeField] GameObject pearlSelectionPrefab;
    [SerializeField] bool havePearl = true;
    [SerializeField] PowerSO powerData;
    public event Action<PearlToObtain> OnDestroy;
    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
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

    public void AddForce(Vector2 vectorForce)
    {
        rigidbody2D.AddForce(vectorForce,ForceMode2D.Impulse);
    }

    private SelectionPearl GenerateSelectionPearl()
    {
        SelectionPearl selectionPearl = Instantiate(pearlSelectionPrefab, transform.position, transform.rotation).GetComponent<SelectionPearl>();
        selectionPearl.Initialize(powerData);
        return selectionPearl;
    }

    IEnumerator DestroyMe()
    {
        OnDestroy?.Invoke(this);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


}
