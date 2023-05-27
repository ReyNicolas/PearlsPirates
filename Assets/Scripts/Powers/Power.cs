using UnityEngine;

public abstract class Power : MonoBehaviour
{
    public abstract void Execute();

    public abstract Color ColorToPearl { get; }
    public abstract float TimeToStart {get;}
}
