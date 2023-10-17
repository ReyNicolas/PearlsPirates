using System;
using System.Collections;
using UnityEngine;

public class PearlToObtain : MonoBehaviour, IDestroy
{
    [SerializeField] GameObject pearlSelectionPrefab;
    [SerializeField] bool havePearl = true;
    [SerializeField] PowerSO powerData;
    [SerializeField] SpriteRenderer spriteRenderer;
    public event Action<PearlToObtain> OnDestroy;
    public event Action<GameObject> onDestroy;

    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void Initialize(PowerSO powerData)
    {
        this.powerData = powerData;
        spriteRenderer.color = powerData.PowerColor;
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

    public void Destroy()
    {
        StartCoroutine(DestroyMe());
    }
}
