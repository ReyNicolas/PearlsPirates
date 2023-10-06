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
    void CountGamepads()
    {
        gamepads = Gamepad.all.Count;
        gamepadCount.text = gamepads.ToString();
        playersPanels.ForEach(pp => pp.gameObject.SetActive(false));
        for (int i = 0; i < gamepads; i++)
        {
            playersPanels[i].gameObject.SetActive(true);
        }
    }

    public void StartMatch()
    {
        if (Gamepad.all.Count == 0)
        {
            errorMessage.text = "Add at least one gamepad";
            return;
        }
        matchData.playersDatas = playersDatas.Take(Gamepad.all.Count).ToList();
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







