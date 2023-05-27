using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PearlCollector : MonoBehaviour
{

    SelectionPearl pearl = null;
   private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PearlToObtain>() && IsEmpty())
        {
            PearlToObtain pearlToObtain = collision.GetComponent<PearlToObtain>();

            if (pearlToObtain.HavePearl())
            {
                SetPearl(pearlToObtain.GiveMeYourPearl());
            }
        }        
    }

    public void SetPearl(SelectionPearl pearl)
    {
        this.pearl = pearl;
        pearl.transform.position = transform.position;
        pearl.transform.rotation = transform.rotation;
        pearl.transform.SetParent(transform);
    }

    public SelectionPearl GetPearl()=> pearl;
    public void SetEmpty()=> pearl = null;

    public bool IsEmpty()=> pearl == null;


}


