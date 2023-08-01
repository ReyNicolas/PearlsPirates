using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    public PositionGenerator positionGenerator = new PositionGenerator();
      public PearlsPointsCalculator pearlsPointsCalculator = new PearlsPointsCalculator();     
    PointsManager pointsManager;

    private void Awake()
    {
        matchData.Initialize();
        
        positionGenerator.SetDimensions(new Vector2(8, 8));
        positionGenerator.SetCenterTransform(Camera.main.transform);
        pointsManager = new PointsManager(matchData.playersDatas, new List<IPlayerPointsGiver>() { pearlsPointsCalculator });
    }



    

}
