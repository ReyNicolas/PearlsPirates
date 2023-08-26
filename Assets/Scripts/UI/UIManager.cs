using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] ResultUI resultUI;
    [SerializeField] MatchSO matchData;
    [SerializeField] List<GameObject> playerPanels;


    private void Start()
    {
        matchData.winnerData.Where(wd=>wd!=null).Subscribe(value => SetResult(value));

        for(int i = 0; i < matchData.playersDatas.Count; i++)
        {
            playerPanels[i].SetActive(true);
        }
    }

    void SetResult(PlayerSO playerData)
    {
        
        resultUI.gameObject.SetActive(true);
        resultUI.ShowResult(matchData);
        
    }
}

