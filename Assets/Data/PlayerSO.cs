using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerSO : ScriptableObject
{
    public Sprite shipSprite;
    public string PlayerName;
    public Color PlayerColor;
    public int PlayerScore;
    public ReactiveProperty<int> PointsToAdd= new ReactiveProperty<int>(0);
    public ReactiveCollection<PowerSO> pearlsCollectedDatas = new ReactiveCollection<PowerSO>();
    public ReactiveDictionary<int, PowerSO> powersInCollectors = new ReactiveDictionary<int, PowerSO> {
        {0,null },
        {1,null },
        {2,null },
        {3,null },
        {4,null },
        {5,null }
    };
    public Vector3 position;
    public ReactiveProperty<float> actualSpeed = new ReactiveProperty<float>(0);

    public void Initialize()
    {
        PlayerScore = 0;
        pearlsCollectedDatas.Clear();
        ResetValuesForRound();

    }

    public void ResetValuesForRound()
    {
        PointsToAdd.Dispose();
        PointsToAdd= new ReactiveProperty<int>(0);
        actualSpeed.Value = 0;
        for (int i = 0; i < powersInCollectors.Count; i++)
        {
            powersInCollectors[i] = null;
        }
    }
}
