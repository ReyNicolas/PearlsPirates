using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] ResultUI resultUI;
    [SerializeField] MatchSO matchData;


    private void Start()
    {
        matchData.winnerData.Subscribe(value => SetResult(value));
    }

    void SetResult(PlayerSO playerData)
    {
        if (playerData!=null) {
            resultUI.gameObject.SetActive(true);
            resultUI.ShowResult(matchData);
        }
    }
}

//public class PlayerResultUI : MonoBehaviour
//{
//    public void ShowResult(PlayerSO playerData)
//    {
        
//    }
//}