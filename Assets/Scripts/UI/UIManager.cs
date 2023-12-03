using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIManager : MonoBehaviour
{
    [SerializeField] ResultsWindowUI resultUI;
    [SerializeField] MatchSO matchData;
    [SerializeField] List<GameObject> playerPanels;
    CompositeDisposable disposables;

    private void Start()
    {
        disposables = new CompositeDisposable(matchData.winnerData.Where(wd => wd != null).Subscribe(value => SetResult(value)));

        for(int i = 0; i < matchData.playersDatas.Count; i++)
        {
            playerPanels[i].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }

    void SetResult(PlayerSO playerData)
    {        
        resultUI.gameObject.SetActive(true);        
    }
}

