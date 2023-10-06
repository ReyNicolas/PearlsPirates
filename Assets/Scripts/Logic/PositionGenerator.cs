using System.Xml.Schema;
using UnityEngine;

public class PositionGenerator
{
    Vector2 dimension;
    float margin = 1f;
    float xMin, xMax;
    float yMin, yMax;
    Camera mainCamera;
    
    public void SetDimension()
    {
        mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        xMin = mainCamera.transform.position.x - cameraWidth / 2;
        xMax = mainCamera.transform.position.x + cameraWidth / 2;

        yMin = mainCamera.transform.position.y - cameraHeight / 2;
        yMax = mainCamera.transform.position.y + cameraHeight / 2;
    }


    public void SetDimensions(Vector2 vector) => 
        dimension = vector;

    public void AddObjectToListen(IGameObjectCreator gameObjectCreator)
    {
        gameObjectCreator.onCreatedInMapGameObject += AssignPosition;
    }

    void AssignPosition(GameObject gameObject)
    {
        gameObject.transform.position = ReturnPosition();
    }


    Vector2 ReturnPosition()
    {
        while (true)
        {
            var position = GenerateRandomPosition();
            if (!Physics2D.BoxCast(position, Vector2.one, 0, Vector2.zero, 0.5f)) return position;
            else Debug.Log("no position");
        }        
    }

    Vector2 GenerateRandomPosition()
    {
        var position = mainCamera.transform.position;
        return new Vector2(position.x + Random.Range(xMin + margin, xMax -margin), position.y + Random.Range(yMin + margin,yMax - margin));
    }
}
