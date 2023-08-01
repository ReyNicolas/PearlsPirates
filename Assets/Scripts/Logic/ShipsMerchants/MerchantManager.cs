using System.Linq;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{
    ColorGenerator colorGenerator;
    ShipPearlsGetterGenerator shipGenerator;
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject shipPrefab;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        colorGenerator = new ColorGenerator(matchData.powersDatas.Select(pd => pd.PowerColor).ToList());

    }
    private void Start()
    {
        shipGenerator = new ShipPearlsGetterGenerator(shipPrefab, gameManager.positionGenerator, gameManager.pearlsPointsCalculator, colorGenerator);
        Test_______GenerateShips();

    }

    void Test_______GenerateShips()
    {
        for (int i = 0; i < 2; i++)
        {
            shipGenerator.ActiveShip();
        }
    }
}
