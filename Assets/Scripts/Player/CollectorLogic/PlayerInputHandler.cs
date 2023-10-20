using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] CannonShoot cannonRight;
    [SerializeField] CannonShoot cannonLeft;
    [SerializeField] ShipMovement shipMovement;
    [SerializeField] ContainerRotate containerRotate;

    private void Update()
    {
        shipMovement.SetMovement(Vector2.ClampMagnitude(playerInput.actions["Move"].ReadValue<Vector2>(), 1f));
    }

    public void RotateRight(CallbackContext context)
    {
        if (context.started) containerRotate.RotateRight();
    }

    public void RotateLeft(CallbackContext context)
    {
        if (context.started) containerRotate.RotateLeft();
    }

    public void ShootCannonRight()
    {
        cannonRight.Shoot();
    }

    public void ShootCannonLeft() 
    { 
        cannonLeft.Shoot();
    }




}