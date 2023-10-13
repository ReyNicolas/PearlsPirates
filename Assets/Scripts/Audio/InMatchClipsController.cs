using System.Linq;
using UniRx;
using UnityEngine;

public class InMatchClipsController : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] AudioClip pointClip;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip disappearClip;
    [SerializeField] DeadZoneLogic deadZoneLogic;
    [SerializeField] AudioManager audioManager;
    CompositeDisposable disposables;

    private void Start()
    {
        deadZoneLogic.OnDeadZone += PlayDeadZoneClip;
        disposables = new CompositeDisposable(

            matchData.winnerData
            .Where(wd => wd != null)
            .Subscribe(wd => PlayInterfaceClip(winClip))
            );

            matchData.playersDatas.ForEach(
                pd =>
                disposables.Add(
                    pd.PointsToAdd
                    .Where(points => points != 0)
                    .Subscribe(pa => PlayInterfaceClip(pointClip)))
            );
    }

    private void OnDestroy()
    {
        deadZoneLogic.OnDeadZone -= PlayDeadZoneClip;
        disposables.Dispose();
    }

    void PlayDeadZoneClip() =>
        PlayInterfaceClip(disappearClip);

    void PlayInterfaceClip(AudioClip audioClip) =>
        audioManager.PlayInterfaceClip(audioClip);

}




