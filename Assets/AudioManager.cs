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
    int playersMoving;

    private void Start()
    {
        StartCoroutine(SetWavesVolume());
        matchData.winnerData.Subscribe(wd => PlayWinnerClip(wd));
        matchData.playersDatas.ForEach(pd => pd.PointsToAdd.Subscribe(pa => PlayPointsClip(pa)));
    }

     void PlayWinnerClip(PlayerSO winnerData)
    {
        if(winnerData!=null)
        {
            musicSource.volume = 0.1f;
            waveSource.volume = 0.1f;

            interfaceSounds.PlayOneShot(winClip);
            interfaceSounds.Play();
        }
    }

    void PlayPointsClip(int points)
    {
        if(points==0) return;
        interfaceSounds.PlayOneShot(pointClip);
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
