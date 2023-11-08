using UnityEngine;

public class PearlSelector : MonoBehaviour
{

    [SerializeField] IShootLogic shootLogic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SelectionPearl>() != null)
        {
            shootLogic.SetPearl(collision.GetComponent<SelectionPearl>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SelectionPearl>() != null)
        {
            shootLogic.SetPearl(null);
        }
    }
}
