using System.Xml.Schema;
using UnityEngine;

public class PositionAsigner
{
    Vector2 dimension;
    Transform centerTransform;
    
    public void SetCenterTransform(Transform transform) =>
        centerTransform = transform;

    public void SetDimensions(Vector2 vector) => 
        dimension = vector;

    public Vector2 ReturnPosition()
    {
        while (true)
        {
            var position = GenerateRandomPosition();
            if(!Physics2D.BoxCast(position, Vector2.one, 0, Vector2.zero, 0.1f))  return position;
        }        
    }

    private Vector2 GenerateRandomPosition()
    {
        var position = centerTransform.position;
        return new Vector2(position.x + Random.Range(-dimension.x, dimension.x), position.y + Random.Range(-dimension.y, dimension.y));
    }
}
