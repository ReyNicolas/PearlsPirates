using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Power Data")]
public class PowerSO : ScriptableObject
{
    public Color PowerColor;
    public GameObject PowerPrefab;
    public float TimeToStart;
    public Sprite sprite;
    [Multiline]
    [TextArea(3, 10)]
    public string Description;
}

