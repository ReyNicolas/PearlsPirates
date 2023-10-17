using System;
using UnityEngine;

public class PearlGenerator
{
    GameObject pearlPrefab;
    public event Action<PearlToObtain> OnCreatedPearlToObtain;
    public event Action<GameObject> onCreatedForMapGameObject;
    public PearlGenerator(GameObject pearlPrefab)
    {
        this.pearlPrefab = pearlPrefab;     
    }

    public void CreatePearl()
    {
        var pearlToObtainGenerated = GameObject.Instantiate(pearlPrefab, Vector3.zero, Quaternion.identity).GetComponent<PearlToObtain>();
        OnCreatedPearlToObtain?.Invoke(pearlToObtainGenerated);
        onCreatedForMapGameObject?.Invoke(pearlToObtainGenerated.gameObject);
    }        
    
}

public interface IGameObjectCreator
{
    event Action<GameObject> onCreatedForMapGameObject;
}
