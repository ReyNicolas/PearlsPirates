using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] AudioSource waveSource;
    int playersMoving;

    private void Start()
    {
        StartCoroutine(SetWavesVolume());
    }

    IEnumerator SetWavesVolume()
    {
        while (true)
        {
            playersMoving = 0;
            yield return new WaitForSeconds(0.1f);
            playersMoving = matchData.playersDatas.Count(pd => pd.actualSpeed.Value > 1);
            waveSource.volume += playersMoving > 0 ? 0.02f * playersMoving : -0.02f;

        }
        
    }
}
