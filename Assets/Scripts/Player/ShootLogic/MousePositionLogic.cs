using UnityEngine;

public class MousePositionLogic: MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float minDistance;
    [SerializeField] float distanceFloat;
    [SerializeField] Transform shootTransform;
    [SerializeField] Vector3 mousePosition;
    [SerializeField] Vector3 vectorDistance;


    private void Update()
    {
        GetMousePosition();
        GetDistanceMouseToShootPosition();
        SetPosition();
    }
       

    void GetMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
    }
    void GetDistanceMouseToShootPosition()
    {
        vectorDistance = shootTransform.position - mousePosition;
        distanceFloat = vectorDistance.magnitude;
        if (distanceFloat > maxDistance) vectorDistance = vectorDistance.normalized * maxDistance;
        else if (distanceFloat < minDistance) vectorDistance = vectorDistance.normalized * minDistance;

    }

    void SetPosition() => transform.position = shootTransform.position - vectorDistance;

}
