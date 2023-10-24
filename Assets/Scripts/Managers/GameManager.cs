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

public class GameManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject optionsGO;
    public PlayerRespawnGenerator respawnGenerator;
    public PositionGenerator positionGenerator = new PositionGenerator();

    CompositeDisposable disposables;

    private void Awake()
    {
        matchData.Initialize();
        SetPositionGenerator();
        SetRespawnGenerator();
        SetHumanPlayers();
        SetBotPlayers();
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
        respawnGenerator.onCreatedForMapGameObject += SetPositionForMapGameobject;
    }

    void SetPositionGenerator()
    {
        positionGenerator.SetDimension();
    }

    void SetPositionForMapGameobject(GameObject gameobject)
    {
        positionGenerator.AssignPosition(gameobject);
    }

    void SetHumanPlayers()
    {
        var gamepadCount = Gamepad.all.Count;
        for (int i = 0; i < math.min(matchData.humansDatas.Count, gamepadCount); i++)
        {
            var shipHumanGO = Instantiate(matchData.playerShipPrefab, Vector3.zero, Quaternion.identity);
            SetPlayerDataInShip(matchData.humansDatas[i], shipHumanGO.GetComponent<PearlCollectorsManager>(), shipHumanGO.GetComponent<ShipMovement>());
            SetPositionForMapGameobject(shipHumanGO);
            respawnGenerator.Listen(shipHumanGO.GetComponent<IDestroy>()); 
            SetInput(i, shipHumanGO.GetComponent<PlayerInput>());
            SetATransformLookToZeroCoord(shipHumanGO.transform);
        }
    }
    void SetBotPlayers()
    {
        for (int i = 0; i < matchData.botsDatas.Count; i++)
        {
            var botShipGO = Instantiate(matchData.botsrShipPrefab, Vector3.zero, Quaternion.identity);
            SetPlayerDataInShip(matchData.botsDatas[i], botShipGO.GetComponent<PearlCollectorsManager>(), botShipGO.GetComponent<ShipMovement>());
            SetBotDataInShip(matchData.botsDatas[i], botShipGO.GetComponent<IAMoveControlLogic>());
            SetPositionForMapGameobject(botShipGO);
            respawnGenerator.Listen(botShipGO.GetComponent<IDestroy>());
            SetATransformLookToZeroCoord(botShipGO.transform);
        }
    }

    void SetATransformLookToZeroCoord(Transform aTransform) 
        => aTransform.up = -aTransform.position;

    void SetPlayerDataInShip(PlayerSO playerData, PearlCollectorsManager collectorsManager, ShipMovement shipMovement)
    {
        collectorsManager.playerData = playerData;
        shipMovement.playerData = playerData;
        shipMovement.GetComponent<SpriteRenderer>().sprite = playerData.shipSprite;
    } 

    void SetBotDataInShip(PlayerSO playerData, IAMoveControlLogic iAMoveControlLogic)
    {
        iAMoveControlLogic.data = playerData;
    }
    void SetInput(int index, PlayerInput playerInput)
    {
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(Gamepad.all[index], user: playerInput.user);
    }

    void StopGame() 
        => Time.timeScale = 0;


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
