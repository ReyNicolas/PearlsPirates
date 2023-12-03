using UnityEngine;

public class ResultsWindowUI : MonoBehaviour
{
    [SerializeField] Transform playersResultsTransform;
    [SerializeField] PlayerResult playerResultPrefab;
    [SerializeField] PlayerResult winnerResult;
    [SerializeField] MatchSO matchData;

    private void OnEnable()
    {
        matchData.playersDatas.ForEach(playerData =>
        {
            Instantiate(playerResultPrefab, playersResultsTransform).Initialize(playerData);
        });

        winnerResult.Initialize(matchData.winnerData.Value);
    }
}

