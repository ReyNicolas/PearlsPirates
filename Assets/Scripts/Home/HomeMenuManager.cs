using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    int gamepads = 0;


    private void Start()
    {
        SetPoinstPlayer();
        SetPoinstMatch();
        CountGamepads();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        CountGamepads();
    }
    public void CountGamepads()
    {
        gamepads = Gamepad.all.Count;
        gamepadCount.text = gamepads.ToString();
        playersPanels.ForEach(pp => pp.gameObject.SetActive(false));
        for (int i = 0; i < gamepads; i++)
        {
            playersPanels[i].gameObject.SetActive(true);
            playersPanels[i].SetName(false);            
        }
        for (int i = gamepads; i < int.Parse(maxPlayers.text); i++)
        {
            playersPanels[i].gameObject.SetActive(true);
            playersPanels[i].SetName(true);
        }


    }

    public void StartMatch()
    {
        if (Gamepad.all.Count == 0)
        {
            errorMessage.text = "Add at least one gamepad";
            return;
        }
        var totalPlayers = int.Parse(maxPlayers.text);

        matchData.humansDatas = playersDatas.Take(Gamepad.all.Count).ToList();
        matchData.playersDatas = playersDatas.Take(totalPlayers).ToList();
        matchData.botsDatas = matchData.playersDatas.Where(pd=> !matchData.humansDatas.Contains(pd)).ToList();
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







