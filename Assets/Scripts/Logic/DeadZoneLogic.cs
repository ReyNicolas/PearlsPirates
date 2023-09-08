using System;
using UnityEngine;

public class DeadZoneLogic : MonoBehaviour
{
    [SerializeField] GameObject DisappearEffectPrefab;
    public event Action OnDeadZone;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDestroy>() != null)
        {
            Destroy(Instantiate(DisappearEffectPrefab, collision.transform.position, Quaternion.identity), 0.6f);
            collision.GetComponent<IDestroy>().Destroy();
            OnDeadZone?.Invoke();
        }

    }
}
