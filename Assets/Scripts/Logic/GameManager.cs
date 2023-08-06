using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XInput;
using Unity.Mathematics;
using System;

public class GameManager: MonoBehaviour, IGameObjectCreator
{
    [SerializeField] MatchSO matchData;
    [SerializeField] List<PlayerSO> playersDatas;
    [SerializeField] GameObject playerShipPrefab;
    public PositionGenerator positionGenerator = new PositionGenerator();
      public PearlsPointsCalculator pearlsPointsCalculator = new PearlsPointsCalculator();     
    PointsManager pointsManager;

    public event Action<GameObject> OnCreatedInMapGameObject;

    private void Awake()
    {
        Application.targetFrameRate = 60; // Establece el máximo de FPS a 60
        matchData.Initialize();

        SetPositionGenerator();
        pointsManager = new PointsManager(matchData.playersDatas, new List<IPlayerPointsGiver>() { pearlsPointsCalculator });
        StartPlayers();
    }

    void StartPlayers()
    {
        var gamepadCount = Gamepad.all.Count;
        for (int i = 0; i < math.min(playersDatas.Count,gamepadCount); i++)
        {
            var shipGO = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
            OnCreatedInMapGameObject?.Invoke(shipGO);
            SetInput(i, shipGO.GetComponent<PlayerInput>());
            SetPlayerDataInCollectorManager(playersDatas[i], shipGO.GetComponent<PearlCollectorsManager>());
            playersDatas[i].Initialize();
        }
    }
    void SetPositionGenerator()
    {
        positionGenerator.SetDimensions(new Vector2(8, 8));
        positionGenerator.SetCenterTransform(Camera.main.transform);
        positionGenerator.AddObjectToListen(this);
    }

    void SetPlayerDataInCollectorManager(PlayerSO playerData, PearlCollectorsManager collectorsManager) 
        => collectorsManager.playerData = playerData;

    void SetInput(int index, PlayerInput playerInput)
    {
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(Gamepad.all[index], user: playerInput.user);
    }
}
