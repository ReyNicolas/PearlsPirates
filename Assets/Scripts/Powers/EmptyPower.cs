using UnityEngine;

public class EmptyPower: Power
{
    [SerializeField] Color color;
    [SerializeField] float timeToStart;

    public override Color ColorToPearl { get { return color; } }

    public override float TimeToStart { get { return timeToStart; } }

    public override void Execute()
    {
       
    }
}