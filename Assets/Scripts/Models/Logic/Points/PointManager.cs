
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointManager: MonoBehaviour
{
    [SerializeField] List<Power> powers = new List<Power>();
    PointsContainer pointsContainer = new PointsContainer();
    PositionAsigner positionAsigner = new PositionAsigner();
    PearlsPointsCalculator pearlsPointsCalculator= new PearlsPointsCalculator();
    ColorGenerator colorGenerator;
    ShipGenerator shipGenerator;

    [SerializeField]GameObject shipPrefab;

    private void Awake()
    {
        positionAsigner.SetDimensions(new Vector2(8, 8));
        positionAsigner.SetCenterTransform(Camera.main.transform);
        Test_______ColorGenerator();
        shipGenerator = new ShipGenerator(shipPrefab, positionAsigner, pearlsPointsCalculator,colorGenerator);
    }

    void Test_______ColorGenerator()
    {
        colorGenerator = new ColorGenerator();
        colorGenerator.AddThisColors(GenerateColors());
    }

    List<Color> GenerateColors()
    {
        var allColors = GeneratePowersColors();
        var colors = new List<Color>();
        for(int i = 0; i < 30; i++)
        {
            colors.Add(allColors[Random.Range(0,allColors.Count-1)]) ;
        }
        return colors;
    }

    List<Color> GeneratePowersColors() => 
        powers.Select(power => power.ColorToPearl).Distinct().ToList();
}
