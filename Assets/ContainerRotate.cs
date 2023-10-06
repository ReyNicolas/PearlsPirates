using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ContainerRotate : MonoBehaviour
{
    [SerializeField] float rotationTime;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;


    public void RotateRight(CallbackContext context)
    {
       if(context.performed) StartCoroutine( RotateThisDegrees(-30));
    }
    public void RotateLeft(CallbackContext context)
    {
        if (context.performed) StartCoroutine( RotateThisDegrees(30));
    }

    IEnumerator RotateThisDegrees(float desgrees)
    {
        audioSource.PlayOneShot(audioClip,0.2f);
        for(int i = 0; i < 6; i++)
        {
            transform.Rotate(0, 0, desgrees / 6);
            yield return new WaitForSeconds(rotationTime / 6);
        }
        
    }
}
