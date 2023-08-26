using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using System;
using Unity.Mathematics;

public class AudioManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource waveSource;
    [SerializeField] AudioSource interfaceSounds;
    [SerializeField] AudioClip pointClip;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip disappearClip;
    [SerializeField] DeadZoneLogic deadZoneLogic;
    int playersMoving;

    private void Start()
    {
        StartCoroutine(SetWavesVolume());
        matchData.winnerData.Where(wd=>wd!=null).Subscribe(wd => PlayWinnerClip(wd));
        matchData.playersDatas.ForEach(pd => pd.PointsToAdd.Where(points=>points!=0).Subscribe(pa => PlayPointsClip(pa)));
        deadZoneLogic.OnDeadZone += PlayDisappearClip;
    }

     void PlayWinnerClip(PlayerSO winnerData)
    {        
        musicSource.volume = 0.1f;
        waveSource.volume = 0.1f;
        interfaceSounds.PlayOneShot(winClip);
        interfaceSounds.Play();
    }

    void PlayPointsClip(int points) => 
        interfaceSounds.PlayOneShot(pointClip);

    void PlayDisappearClip() =>
        interfaceSounds.PlayOneShot(disappearClip);

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
