using UnityEngine;
using System;

public class ContainerRotate : MonoBehaviour
{
    [SerializeField] int degreesPerSecond;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] float degrees;

    private void Update()
    {
        if (degrees != 0) Rotate();
    }

    public void RotateRight()
    {
         AddRotation( -60 );
    }

  
    public void RotateLeft()
    {
         AddRotation(60);
    }
    void AddRotation(float degrees)
    {
        audioSource.PlayOneShot(audioClip, 0.2f);
        this.degrees += degrees;
    }


    void Rotate()
    {
        float degreesDelta = (degrees > 0)
            ? MathF.Min(degrees, degreesPerSecond * Time.deltaTime)
            : Math.Max(degrees, -degreesPerSecond * Time.deltaTime);
        transform.Rotate(Vector3.forward * degreesDelta);
        degrees -= degreesDelta;
        return;  
    }
}
