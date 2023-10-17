using System;
using UnityEngine;

public class PlayerRespawnGenerator : IGameObjectCreator
{
    GameObject instantPearlPrefab;
    public event Action<GameObject> onCreatedForMapGameObject;
    public PlayerRespawnGenerator(GameObject instantPearlPrefab)
    {
        this.instantPearlPrefab = instantPearlPrefab;
    }

    public void Listen(IDestroy contentToListen)
    {
        contentToListen.onDestroy += CreateInstantPearl;
    }

    public void CreateInstantPearl(GameObject content)
    {
        var instantPearlGenerated = GameObject.Instantiate(instantPearlPrefab, Vector3.zero, Quaternion.identity).GetComponent<InstantPearl>();
        onCreatedForMapGameObject?.Invoke(instantPearlGenerated.gameObject);
        instantPearlGenerated.SetContent(content);
       
    }

}
