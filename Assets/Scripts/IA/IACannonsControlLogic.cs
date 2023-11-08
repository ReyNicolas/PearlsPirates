using UnityEngine;

public class IACannonsControlLogic : MonoBehaviour
{
    [SerializeField] float fireControlRate;
    [SerializeField] LayerMask layerMask;
    [SerializeField] CannonShoot cannonShootRight;
    [SerializeField] CannonShoot cannonShootLeft;
    float timer;
    // Start is called before the first frame update
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) Attack();
    }

    void Attack()
    {
        if (Physics2D.OverlapCircle(transform.position + (transform.right * 3), 2 ,layerMask))
        {
            cannonShootRight.Shoot();
        }
        if (Physics2D.OverlapCircle(transform.position - (transform.right * 3), 2, layerMask))
        {
            cannonShootLeft.Shoot();
        }
        timer = fireControlRate;
    }
}
