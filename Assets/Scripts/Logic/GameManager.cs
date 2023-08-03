using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XInput;

public class GameManager: MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] List<PlayerSO> playersDatas;
    [SerializeField] GameObject playerShipPrefab;
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


    private void Start()
    {
        var gamepadCount = Gamepad.all.Count;
        for (int i = 0; i < playersDatas.Count; i++)
        {
           var playerInput =  Instantiate(playerShipPrefab, positionGenerator.ReturnPosition(), Quaternion.identity).GetComponent<PlayerInput>();
            playerInput.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(Gamepad.all[i], user: playerInput.user);
        }
    }

}
