using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Unity.Mathematics;
using System;
using UniRx;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour, IGameObjectCreator
{
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject optionsGO;
    public PlayerRespawnGenerator respawnGenerator;
    public PositionGenerator positionGenerator = new PositionGenerator();
    CompositeDisposable disposables;
    PointsManager pointsManager;
    List<PlayerSO> playersDatas;

    public event Action<GameObject> onCreatedInMapGameObject;

    private void Awake()
    {
        playersDatas = matchData.playersDatas;
        matchData.Initialize();
        SetPositionGenerator();
        SetPointManager();
        SetRespawnGenerator();
        SetPlayers();
    }

    private void Start()
    {
        disposables = new CompositeDisposable(
            matchData.winnerData
            .Where(winner => winner != null)
            .Subscribe(_ => StopGame()));

        matchData.playersDatas.ForEach(playerData
            => disposables.Add(  // add disposable playerData
                       playerData.PointsToAdd
                       .Subscribe(value => matchData.CheckWinner(value)))
            );
    }


    private void OnDestroy()
    {
        disposables.Dispose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsGO.SetActive(!optionsGO.activeSelf);
        }
    }

    void SetRespawnGenerator()
    {
        respawnGenerator = new PlayerRespawnGenerator(matchData.instantPearlPrefab);
        positionGenerator.AddObjectToListen(respawnGenerator);
    }

    void SetPositionGenerator()
    {
        positionGenerator.SetDimension();
        positionGenerator.AddObjectToListen(this);
    }
    void SetPointManager()
        => pointsManager = new PointsManager(matchData.playersDatas);
    void SetPlayers()
    {
        var gamepadCount = Gamepad.all.Count;
        for (int i = 0; i < math.min(playersDatas.Count, gamepadCount); i++)
        {
            var shipGO = Instantiate(matchData.playerShipPrefab, Vector3.zero, Quaternion.identity);
            SetPlayerDataInShip(playersDatas[i], shipGO.GetComponent<PearlCollectorsManager>(), shipGO.GetComponent<ShipMovement>());
            onCreatedInMapGameObject?.Invoke(shipGO);
            respawnGenerator.Listen(shipGO.GetComponent<IDestroy>());
            SetInput(i, shipGO.GetComponent<PlayerInput>());
            SetATransformLookToZeroCoord(shipGO.transform);
        }
    }
    void SetATransformLookToZeroCoord(Transform aTransform)
    {
        aTransform.up = - aTransform.position;
    }

    void SetPlayerDataInShip(PlayerSO playerData, PearlCollectorsManager collectorsManager, ShipMovement shipMovement)
    {
        collectorsManager.playerData = playerData;
        shipMovement.playerData = playerData;
        shipMovement.GetComponent<SpriteRenderer>().sprite = playerData.shipSprite;
    } 
    void SetInput(int index, PlayerInput playerInput)
    {
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(Gamepad.all[index], user: playerInput.user);
    }

    void StopGame()
    {       
        Time.timeScale = 0;        
    }


    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(matchData.matchScene);
    }
    public void ReturnHomeMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(matchData.homeMenuScene);
    }
   
   

   
   
}
