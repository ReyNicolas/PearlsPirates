using UnityEngine;

public class PointManager: MonoBehaviour
{
    PointsContainer pointsContainer = new PointsContainer();
    PositionAsigner positionAsigner = new PositionAsigner();
    ShipGenerator shipGenerator;

    [SerializeField]GameObject shipPrefab;

    private void Awake()
    {
        positionAsigner.SetDimensions(new Vector2(8, 8));
        positionAsigner.SetCenterTransform(Camera.main.transform);
        shipGenerator = new ShipGenerator(shipPrefab, positionAsigner);
    }

}
