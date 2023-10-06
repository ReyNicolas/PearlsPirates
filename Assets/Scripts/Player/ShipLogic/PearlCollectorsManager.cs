using System.Collections.Generic;
using System.Linq;
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
        if (collision.gameObject.GetComponent<PearlToObtain>())
        {
           var collector =  collectors.OrderBy(c=> (collision.transform.position- c.transform.position).magnitude).ToList().Find(c=>c.IsEmpty());
            if (collector != null) SetPearlToCollector(collision.gameObject.GetComponent<PearlToObtain>().GiveMeYourPearl(), collector);
            return;
        }

        if (collision.gameObject.GetComponent<IMarket>() != null)
        {
            collision.gameObject.GetComponent<IMarket>().TryToCollectThisPearlsFromThisPlayerData(GetPearls(), playerData);
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
