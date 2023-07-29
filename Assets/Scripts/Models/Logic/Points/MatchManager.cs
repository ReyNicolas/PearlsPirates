
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchManager: MonoBehaviour
{
    [SerializeField] List<PowerSO> powersDatas = new List<PowerSO>();
    [SerializeField] List<PlayerSO> playersDatas = new List<PlayerSO>();
    PointsManager pointsManager;
    PositionAsigner positionAsigner = new PositionAsigner();
    PearlsPointsCalculator pearlsPointsCalculator= new PearlsPointsCalculator();
    ColorGenerator colorGenerator = new ColorGenerator();
    ShipPearlsGetterGenerator shipGenerator;

    [SerializeField]GameObject shipPrefab;

    private void Awake()
    {
        pointsManager = new PointsManager(playersDatas, new List<IPlayerPointsGiver>() { pearlsPointsCalculator});
     
        positionAsigner.SetDimensions(new Vector2(8, 8));
        positionAsigner.SetCenterTransform(Camera.main.transform);  
        
        Test_______ColorGenerator();
        shipGenerator = new ShipPearlsGetterGenerator(shipPrefab, positionAsigner, pearlsPointsCalculator,colorGenerator);
        Test_______GenerateShips();
    }

    void Test_______ColorGenerator()
    {
        colorGenerator.AddThisColors(GenerateColors());
    }

    void Test_______GenerateShips()
    {
        for(int i = 0; i<5; i++)
        {

        shipGenerator.ActiveShip();
        }
    }

    List<Color> GenerateColors()
    {
        var allColors = GeneratePowersColors();
        var colors = new List<Color>();
        for(int i = 0; i < 30; i++)
        {
            colors.Add(allColors[Random.Range(0,allColors.Count)]) ;
        }
        return colors;
    }

    List<Color> GeneratePowersColors() =>
        powersDatas.Select(powerData => powerData.PowerColor).Distinct().ToList();
}
