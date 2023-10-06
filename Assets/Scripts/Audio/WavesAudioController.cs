using System.Collections;
using System.Linq;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

public class WavesAudioController: MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] AudioSource waveSource;
    CompositeDisposable disposables;

    int playersMoving;

    private void OnEnable()
    {
        disposables = new CompositeDisposable(
            matchData.winnerData
            .Where(wd => wd != null)
            .Subscribe(_ => waveSource.Stop())
            );
        StartCoroutine(SetWavesVolume());    
    }

    private void OnDisable()
    {
        disposables.Dispose(); 
    }
    IEnumerator SetWavesVolume()
    {
        while (true)
        {
            playersMoving = 0;
            yield return new WaitForSeconds(0.1f);
            playersMoving = matchData.playersDatas.Count(pd => pd.actualSpeed.Value > 1);
            waveSource.volume += playersMoving > 0 ? 0.05f * playersMoving : -0.05f;
            waveSource.volume = math.max(waveSource.volume, 0.1f);
        }

    }
}




