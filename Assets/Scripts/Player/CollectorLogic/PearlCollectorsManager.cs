using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PearlCollectorsManager : MonoBehaviour
{
    [SerializeField] List<PearlCollector> collectors;
    [SerializeField] AudioClip rotateClip;
    PlayerInput playerInput;
    public PlayerSO playerData;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        collectors.ForEach(c => c.playerData = playerData);
    }

    private void Update()
    {
        playerData.position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if  (collision.gameObject.TryGetComponent<PearlToObtain>(out var pearlToObtain))
        {
           var collector =  collectors.OrderBy(c=> (collision.transform.position- c.transform.position).magnitude).ToList().Find(c=>c.IsEmpty());
            if (collector != null) SetPearlToCollector(pearlToObtain.GiveMeYourPearl(), collector);
            return;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent<IMarket>(out var market))
        {
            market.TryToCollectThisPearlsFromThisPlayerData(GetPearls(), playerData);
        }
    }




   List<SelectionPearl> GetPearls()
    {
        return collectors.Where(c=>!c.IsEmpty()).Select(c => c.pearl).ToList();
    }

    void SetPearlToCollector(SelectionPearl pearl, PearlCollector collector)
    {
        pearl.transform.position = collector.transform.position;
        pearl.transform.SetParent(collector.transform);
    }


}
