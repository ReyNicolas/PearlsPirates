using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Unity.Mathematics;
using System;
using UniRx;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour, IGameObjectCreator
{
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject optionsGO;
    public  PlayerRespawnGenerator respawnGenerator;
    public PositionGenerator positionGenerator = new PositionGenerator();
    public PearlsPointsCalculator pearlsPointsCalculator = new PearlsPointsCalculator();     
    PointsManager pointsManager;
    List<PlayerSO> playersDatas;

    public event Action<GameObject> OnCreatedInMapGameObject;

    private void Awake()
    {
        playersDatas = matchData.playersDatas;
        
       
        matchData.Initialize();
        SetPositionGenerator();
        SetPointManager();
        SetRespawnGenerator();
        StartPlayers();
        matchData.winnerData.Where(winner=> winner != null).Subscribe(_ => StopGame());
    }

    private void Update()// TODO: es para probar opciones
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
        => pointsManager = new PointsManager(matchData.playersDatas, new List<IPlayerPointsGiver>() { pearlsPointsCalculator });
    void StartPlayers()
    {
        var gamepadCount = Gamepad.all.Count;
        for (int i = 0; i < math.min(playersDatas.Count, gamepadCount); i++)
        {
            var shipGO = Instantiate(matchData.playerShipPrefab, Vector3.zero, Quaternion.identity);
            SetPlayerDataInShip(playersDatas[i], shipGO.GetComponent<PearlCollectorsManager>(), shipGO.GetComponent<ShipMovement>());            
            OnCreatedInMapGameObject?.Invoke(shipGO);
            respawnGenerator.Listen(shipGO.GetComponent<IDestroy>());
            SetInput(i, shipGO.GetComponent<PlayerInput>());           
        }
    }
    void SetPlayerDataInShip(PlayerSO playerData, PearlCollectorsManager collectorsManager, ShipMovement shipMovement)
    {
        playerData.Initialize();
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
        SceneManager.LoadScene(matchData.matchScene);
    }
    public void ReturnHomeMenu()
    {
        SceneManager.LoadScene(matchData.homeMenuScene);
    }
   
   

   
   
}
