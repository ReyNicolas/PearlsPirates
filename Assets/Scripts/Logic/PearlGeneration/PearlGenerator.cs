using System;
using UnityEngine;

public class PearlGenerator: IGameObjectCreator
{
    GameObject pearlPrefab;
    public event Action<PearlToObtain> OnCreatedPearlToObtain;
    public event Action<GameObject> onCreatedInMapGameObject;
    public PearlGenerator(GameObject pearlPrefab)
    {
        this.pearlPrefab = pearlPrefab;     
    }

    public void CreatePearl()
    {
        var pearlToObtainGenerated = GameObject.Instantiate(pearlPrefab, Vector3.zero, Quaternion.identity).GetComponent<PearlToObtain>();
        OnCreatedPearlToObtain?.Invoke(pearlToObtainGenerated);
        onCreatedInMapGameObject?.Invoke(pearlToObtainGenerated.gameObject);
    }        
    
}

public interface IGameObjectCreator
{
    event Action<GameObject> onCreatedInMapGameObject;
}
