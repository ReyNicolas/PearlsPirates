using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HomeMenuManager : MonoBehaviour
{
    [SerializeField] List<PlayerSO> playersDatas;
    [SerializeField] List<HomePlayerPanel> playersPanels;
    [SerializeField] MatchSO matchData;
    [SerializeField] TextMeshProUGUI playerPointsLabel;
    [SerializeField] TextMeshProUGUI matchPointsLabel;
    [SerializeField] TextMeshProUGUI gamepadCount;
    [SerializeField] TextMeshProUGUI maxPlayers;
    [SerializeField] TextMeshProUGUI errorMessage;
    [SerializeField] List<string> keyboards = new List<string>();
    readonly string KEYBOARD1 = "Keyboard1";
    readonly string KEYBOARD2 = "Keyboard2";
    int gamepads, playerCount;


    private void Start()
    {
        SetPoinstPlayer();
        SetPoinstMatch();
        CountPlayers();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (keyboards.Contains(KEYBOARD1))
            {
                keyboards.Remove(KEYBOARD1);
            }
            else
            {
                keyboards.Add(KEYBOARD1);
            }

            CountPlayers();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (keyboards.Contains(KEYBOARD2))
            {
                keyboards.Remove(KEYBOARD2);
            }
            else
            {
                keyboards.Add(KEYBOARD2);
            }
            CountPlayers();
        }
    }


    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        CountPlayers();
    }
    public void CountPlayers()
    {
        gamepads = Gamepad.all.Count;
        gamepadCount.text = gamepads.ToString();

        playerCount = keyboards.Count + gamepads;
        
        playersPanels.ForEach(pp => pp.gameObject.SetActive(false));

        for (int i = 0; i < int.Parse(maxPlayers.text); i++)
        {
            playersPanels[i].gameObject.SetActive(true);

            if (i < keyboards.Count)
            {
                playersDatas[i].InputDevice = keyboards[i];
            }
            else if (i < playerCount)
            {
                playersDatas[i].InputDevice = "Gamepad" + (i + 1 - keyboards.Count);
            }
            else
            {
                playersDatas[i].InputDevice = "Bot";
            }

            playersPanels[i].SetMyPlayer(playersDatas[i]);          
        }
    }

    public void StartMatch()
    {
        if (playerCount == 0)
        {
            errorMessage.text = "Add at least one gamepad or keyboard player";
            return;
        }
        var totalPlayers = int.Parse(maxPlayers.text);
        matchData.playersDatas = playersDatas.Take(totalPlayers).ToList();
        InputSystem.onDeviceChange -= OnDeviceChange;
        matchData.Initialize();
        SceneManager.LoadScene(matchData.matchScene);
    }

    public void SetPoinstPlayer()
    {
        matchData.totalPlayerPointsLimit = int.Parse(playerPointsLabel.text);
    }

    public void SetPoinstMatch()
    {
        matchData.totalPointsLimit = int.Parse(matchPointsLabel.text);
    }
}







