using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HomeMenuManager : MonoBehaviour
{
    [SerializeField] List<PlayerSO> playersDatas;   
    [SerializeField] MatchSO matchData;
    [SerializeField] TextMeshProUGUI playerPointsLabel;
    [SerializeField] TextMeshProUGUI matchPointsLabel;
    [SerializeField] TextMeshProUGUI gamepadCount;
    [SerializeField] TextMeshProUGUI errorMessage;

    private void Start()
    {
        SetPoinstMatch();
        SetPoinstMatch();
        StartCoroutine(CountGamepads());
    }

    IEnumerator CountGamepads()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            gamepadCount.text = Gamepad.all.Count.ToString();
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
        SceneManager.LoadScene("MatchScene");
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
