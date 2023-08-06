using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] float acSpeed = 10.0f;
    [SerializeField] float turnSpeed = 10.0f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField]  float actualSpeed = 0;
    [SerializeField]  float timeToWaitReduce = 0.2f;
    [SerializeField] Vector2 movement;
    [SerializeField] PlayerInput playerInput;
    public PlayerSO playerData;

   
    private void Start()
    {
        InvokeRepeating("ReduceVelocities", 0, timeToWaitReduce);

    }

    private void Update()
    {
        movement = Vector2.ClampMagnitude(playerInput.actions["Move"].ReadValue<Vector2>(), 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude>=1) GetComponent<AudioSource>().Play();
    }

    
    private void FixedUpdate()
    {
        MoveShip(movement);        
    }

    void MoveShip(Vector2 direction)
    {
        rigidbody2D.AddForce(transform.up * direction.y * acSpeed * Time.deltaTime, ForceMode2D.Impulse);
        rigidbody2D.AddTorque(-direction.x * turnSpeed * Time.deltaTime,ForceMode2D.Impulse);
        CheckSpeedLimit();
    }

    void CheckSpeedLimit()
    {
        actualSpeed = rigidbody2D.velocity.magnitude;
        if (actualSpeed > maxSpeed)rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
        playerData.actualSpeed.Value = actualSpeed;
    }
          

    void ReduceVelocities()
    {       
        rigidbody2D.angularVelocity *=  0.7f;
        rigidbody2D.velocity *=  0.8f;
    }

}
