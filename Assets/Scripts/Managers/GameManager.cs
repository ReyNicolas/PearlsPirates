using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UniRx;
using UnityEngine.SceneManagement;

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
        SetPlayers();
    }

    private void Start()
    {
        SetDisposables();
    }
    void SetDisposables()
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
    void SetPlayers()
    {
        foreach (PlayerSO playerData in matchData.playersDatas)
        {
            GameObject shipGO;
            if (playerData.InputDevice.Contains("Keyboard"))
            {
                shipGO = Instantiate(matchData.playerShipPrefab, Vector3.zero, Quaternion.identity);
                SetKeyboardPlayer(playerData, shipGO);
            }
            else if (playerData.InputDevice.Contains("Gamepad"))
            {
                shipGO = Instantiate(matchData.playerShipPrefab, Vector3.zero, Quaternion.identity);

                SetGamepadPlayer(playerData, shipGO);
            }
            else
            {
                shipGO = Instantiate(matchData.botsrShipPrefab, Vector3.zero, Quaternion.identity);

                SetBotDataInShip(playerData, shipGO.GetComponent<IAMoveControlLogic>());
            }

            SetPlayerDataInShip(playerData, shipGO.GetComponent<PearlCollectorsManager>(), shipGO.GetComponent<ShipMovement>());
            SetPositionForMapGameobject(shipGO);
            respawnGenerator.Listen(shipGO.GetComponent<IDestroy>());
            SetATransformLookToZeroCoord(shipGO.transform);
        }
    }

    static void SetKeyboardPlayer(PlayerSO playerData, GameObject shipGO)
    {
        var playerInput = shipGO.GetComponent<PlayerInput>();
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(InputSystem.devices[0], user: playerInput.user);
        playerInput.SwitchCurrentActionMap(playerData.InputDevice);
        playerInput.defaultActionMap = playerData.InputDevice;
    }

    static void SetGamepadPlayer(PlayerSO playerData, GameObject shipGO)
    {
        var playerInput = shipGO.GetComponent<PlayerInput>();
        playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(Gamepad.all[int.Parse(playerData.InputDevice.Replace("Gamepad", "")) - 1], user: playerInput.user);
        playerInput.SwitchCurrentActionMap("Gamepad");
        playerInput.defaultActionMap = "Gamepad";
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
