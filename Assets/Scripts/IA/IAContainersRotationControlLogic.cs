using UnityEngine;

public class IAContainersRotationControlLogic : MonoBehaviour
{
    [SerializeField] int minTime = 2;
    [SerializeField] int maxTime = 4;
    [SerializeField] ContainerRotate containerRotate;
    float timer;


    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) RotateRandomDirection();
    }

    private void RotateRandomDirection()
    {
        timer = Random.Range(minTime, maxTime + 1);
        if (Random.Range(0, 2) == 0)
        {
            containerRotate.RotateLeft();
            return;
        }
        containerRotate.RotateRight();
    }
}
