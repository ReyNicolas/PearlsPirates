using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] GameObject optionsGO;
    public PlayerRespawnGenerator respawnGenerator;
    public PositionGenerator positionGenerator = new PositionGenerator();
    InputSetterManager inputSetterManager = new InputSetterManager();
    PlayerGenerator playerGenerator;

    CompositeDisposable disposables;

    private void Awake()
    {
        matchData.Initialize();
        SetPositionGenerator();
        SetRespawnGenerator();
        SetPlayerGenerator();
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
        => positionGenerator.SetDimension();

    void SetPlayerGenerator() 
        => playerGenerator = new PlayerGenerator(respawnGenerator, positionGenerator, inputSetterManager, matchData);

    void SetPositionForMapGameobject(GameObject gameobject) 
        => positionGenerator.AssignPosition(gameobject);

    void SetPlayers() 
        => matchData.playersDatas
            .ForEach(
            playerData 
            => playerGenerator.GeneratePlayerFromData(playerData));

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
