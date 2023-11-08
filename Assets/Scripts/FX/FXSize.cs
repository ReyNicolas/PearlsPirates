using UnityEngine;

public class FXSize : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float standardSize = 0.5f;

    void Update()
    {
        transform.localScale = Vector3.one * curve.Evaluate(Time.time) * standardSize;
    }
}
