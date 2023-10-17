using System.Collections;
using UnityEngine;

public class AimLogic: MonoBehaviour
{
    [SerializeField] Transform toAimTransform;
    [SerializeField] float speed;


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, toAimTransform.position, speed * Time.deltaTime);
    }

    public Vector3 GetAimPosition()=>transform.position;

    public void WaitThisSeconds(float seconds)=> StartCoroutine(Wait(seconds));

    IEnumerator Wait(float seconds)
    {
        float actualSpeed = speed;
        speed = 0f;
        yield return new WaitForSeconds(seconds);
        speed= actualSpeed;        
    }



}
