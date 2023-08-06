using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XInput;
using Unity.Mathematics;
using System;
using UniRx;

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
        SetPointManager();
        StartPlayers();
        matchData.winnerData.Subscribe(value => StopGameIfThereIsWinner(value));
    }
    void SetPositionGenerator()
    {
        positionGenerator.SetDimensions(new Vector2(8, 8));
        positionGenerator.SetCenterTransform(Camera.main.transform);
        positionGenerator.AddObjectToListen(this);
    }
    void SetPointManager()
        => pointsManager = new PointsManager(matchData.playersDatas, new List<IPlayerPointsGiver>() { pearlsPointsCalculator });
    void StartPlayers()
    {
        var gamepadCount = Gamepad.all.Count;
        for (int i = 0; i < math.min(playersDatas.Count, gamepadCount); i++)
        {
            var shipGO = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
            OnCreatedInMapGameObject?.Invoke(shipGO);
            SetInput(i, shipGO.GetComponent<PlayerInput>());
            SetPlayerDataInShip(playersDatas[i], shipGO.GetComponent<PearlCollectorsManager>(),shipGO.GetComponent<ShipMovement>());
            playersDatas[i].Initialize();
        }
    }
    void SetPlayerDataInShip(PlayerSO playerData, PearlCollectorsManager collectorsManager, ShipMovement shipMovement)
    {
        collectorsManager.playerData = playerData;
        shipMovement.playerData = playerData;
    } 
    void SetInput(int index, PlayerInput playerInput)
    {
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(Gamepad.all[index], user: playerInput.user);
    }

    void StopGameIfThereIsWinner(PlayerSO playerData)
    {
        if(playerData != null)
        {
            Time.timeScale = 0;
        }
    }

   
   
   
   
}
