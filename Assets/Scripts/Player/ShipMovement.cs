using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour, IDestroy
{
    [Header("Movement info")]
    [SerializeField] float acSpeed = 10.0f;
    [SerializeField] float turnSpeed = 10.0f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float actualSpeed = 0;
    [SerializeField] float timeToWaitReduce = 0.2f;
    [SerializeField] float minTorque =0.5f;
    [SerializeField] Vector2 movement;
    [Header("References")]
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] PlayerInput playerInput;
    public PlayerSO playerData;

    public event Action<GameObject> onDestroy;

    private void Start()
    {
        InvokeRepeating("ReduceVelocities", 0, timeToWaitReduce);
        InvokeRepeating("Particles", 0, 0.1f);
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
        if (CheckSpeedLimit()) return;
        rigidbody2D.AddForce(transform.up * direction.y * acSpeed * Time.deltaTime, ForceMode2D.Impulse);
        if (minTorque<math.abs(direction.x)) rigidbody2D.AddTorque(-direction.x * turnSpeed * Time.deltaTime,ForceMode2D.Impulse);
    }

    bool CheckSpeedLimit()
    {
        actualSpeed = rigidbody2D.velocity.magnitude;
        playerData.actualSpeed.Value = actualSpeed;
        //if (actualSpeed > maxSpeed)rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
        return actualSpeed > maxSpeed;
    }
          

    void ReduceVelocities()
    {       
        rigidbody2D.angularVelocity *=  0.7f;
        rigidbody2D.velocity *=  0.8f;
    }

    void Particles()
    {
        particleSystem.Emit(Mathf.RoundToInt(actualSpeed - 0.5f));
    }

    public void Destroy()
    {
        onDestroy?.Invoke(gameObject);
        particleSystem.Clear();
        gameObject.SetActive(false);
    }
}
