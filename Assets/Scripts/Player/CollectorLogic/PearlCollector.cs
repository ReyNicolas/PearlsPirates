using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

public class PearlCollector : MonoBehaviour
{
    [SerializeField] int id;
    public PlayerSO playerData;
    public SelectionPearl pearl = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SelectionPearl>() && IsEmpty())
        {
            SetPearl(collision.GetComponent<SelectionPearl>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<SelectionPearl>() == pearl)
        {
            SetEmpty();
        }
    }

    void SetPearl(SelectionPearl pearl)
    {
        this.pearl = pearl;
        playerData.powersInCollectors[id] = pearl.GetPowerData();
    }

    public SelectionPearl GetPearl()=> 
        pearl;
    public void SetEmpty()
    {
        pearl = null;
        playerData.powersInCollectors[id] = null;
    }
        

    public bool IsEmpty()=> 
        pearl == null;


}


