using UnityEngine;

public class CanonLook : MonoBehaviour
{
    [SerializeField]Transform aimTransform;

    private void Update()
    {
        Vector2 direction = aimTransform.position - transform.position;
        transform.up= direction;
    }


}
