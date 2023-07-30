using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerSO : ScriptableObject
{
    public string PlayerName;
    public Color PlayerColor;
    public int PlayerScore;
    public int PointsToAdd;
    public List<PearlCollectedDTO> pearlsCollectedDatas;
}
