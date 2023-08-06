using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winnerText;
    public void ShowResult(MatchSO matchData)
    {
        SetWinner(matchData.winnerData.Value);
    }

    void SetWinner(PlayerSO playerData)
    {
        winnerText.text = playerData.PlayerName;
        winnerText.color = playerData.PlayerColor;
    }
}

//public class PlayerResultUI : MonoBehaviour
//{
//    public void ShowResult(PlayerSO playerData)
//    {
        
//    }
//}