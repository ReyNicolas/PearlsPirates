
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchManager: MonoBehaviour
{
    [SerializeField] GameObject shipPrefab;
    [SerializeField] GameObject pearlPrefab;
    [SerializeField] MatchSO matchData;
    [SerializeField] List<PowerSO> powersDatas = new List<PowerSO>();
    [SerializeField] List<PlayerSO> playersDatas = new List<PlayerSO>();
    PointsManager pointsManager;
    PearlsPointsCalculator pearlsPointsCalculator = new PearlsPointsCalculator();

    PearGenerator pearGenerator;
    PowerGenerator powerGenerator;
    PositionGenerator positionGenerator = new PositionGenerator();
    ColorGenerator colorGenerator;
    ShipPearlsGetterGenerator shipGenerator;
    PearlsInMatchController pearlsInMatchController;

 

    private void Awake()
    {
        matchData.Initialize(playersDatas, 100, 30, 30);
        
        positionGenerator.SetDimensions(new Vector2(8, 8));
        positionGenerator.SetCenterTransform(Camera.main.transform);

        colorGenerator = new ColorGenerator(powersDatas.Select(pd=> pd.PowerColor).ToList());
        pointsManager = new PointsManager(playersDatas, new List<IPlayerPointsGiver>() { pearlsPointsCalculator });
        powerGenerator = new PowerGenerator(powersDatas);
        pearGenerator = new PearGenerator(pearlPrefab, positionGenerator,powerGenerator);
        shipGenerator = new ShipPearlsGetterGenerator(shipPrefab, positionGenerator, pearlsPointsCalculator,colorGenerator);
        pearlsInMatchController = new PearlsInMatchController(matchData, pearGenerator);
        Test_______GenerateShips();
    }



    void Test_______GenerateShips()
    {
        for(int i = 0; i<5; i++)
        {
          shipGenerator.ActiveShip();
        }
    }

}
