using UnityEngine;
using UnityEngine.InputSystem;

public class DotPositionLogic : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float minDistance;
    [SerializeField] float speed;
    [SerializeField] float distanceFloat;
    [SerializeField] Transform shootTransform;
    [SerializeField] Vector3 vMouse;
    [SerializeField] Vector3 vectorDistance;
    [SerializeField] PlayerInput playerInput;


    private void Update()
    {
        MoveDot();
        GetDistanceMouseToShootPosition();
        SetPosition();
    }
    void MoveDot()
    {
        vMouse = Vector2.ClampMagnitude(playerInput.actions["Aim"].ReadValue<Vector2>(), 1f);
        transform.position += (vMouse * speed * Time.deltaTime);
    }
    void GetDistanceMouseToShootPosition()
    {
        vectorDistance = shootTransform.position - transform.position;
        distanceFloat = vectorDistance.magnitude;
        if (distanceFloat > maxDistance) vectorDistance = vectorDistance.normalized * maxDistance;
        else if (distanceFloat < minDistance) vectorDistance = vectorDistance.normalized * minDistance;

    }

    void SetPosition() => transform.position = shootTransform.position - vectorDistance;

}
