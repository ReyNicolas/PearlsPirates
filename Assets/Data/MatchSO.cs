using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "MatchData", menuName = "Match Data")]
public class MatchSO : ScriptableObject
{
    [Header("Data")]
    public string homeMenuScene;
    public string matchScene;
    public List<PlayerSO> playersDatas;
    public List<PowerSO> powersDatas;
    public GameObject playerShipPrefab;
    public GameObject pearlPrefab;
    public GameObject instantPearlPrefab;
    [Header("End conditions Settings")]
    public int totalPointsLimit;
    public int totalPlayerPointsLimit;
    public float timeLeft;
    [Header("Pearls Settings")]
    public int numberPearlsToObtainInScene;
    public int timeToGeneratePearl;
    public int maxNumberOfPearls;
    [Header("Merchants Settings")]
    public int merchantsInScene;
    public int timeToGenerateMerchants;
    public int maxNumberOfMerchants;
    [Header("Wind Settings")]    
    public Vector2 wind;
    [Header("Sound Settings")]
    public AudioMixer mixer;
    public ReactiveProperty<float> masterVolume = new ReactiveProperty<float>(1);
    public ReactiveProperty<float> musicVolume = new ReactiveProperty<float>(1);
    public ReactiveProperty<float> soundEffectsVolume = new ReactiveProperty<float>(1);
    public ReactiveProperty<float> interfaceVolume = new ReactiveProperty<float>(1);
    public float volumeMultiplier;



    public ReactiveProperty<PlayerSO> winnerData = new ReactiveProperty<PlayerSO>(null);

    public void DisposeMixer()
    {
        masterVolume.Dispose();
        musicVolume.Dispose();
        soundEffectsVolume.Dispose();
        interfaceVolume.Dispose();
    }
    public void SetAudioMixer()
    {
        masterVolume.Subscribe(value=> SetAudio("master", value/100));
        musicVolume.Subscribe(value=> SetAudio("bgm", value/100));
        soundEffectsVolume.Subscribe(value=> SetAudio("sfx", value / 100));
        interfaceVolume.Subscribe(value=> SetAudio("sui", value/ 100));
    }
    void SetAudio(string param,float value) 
        => mixer.SetFloat(param, Mathf.Log10(value) * volumeMultiplier);

    public void Initialize()
    {        

        numberPearlsToObtainInScene = 0;
        playersDatas.ForEach(pd =>  pd.PointsToAdd.Subscribe(value => CheckWinner(value)));
        winnerData.Value = null;
    }

    void CheckWinner(int value)
    {
        if (EndedByTotalPointsLimit() || EndedByTotalPlayerPointsLimit())
        {
            SetWinner();
        }
    }

    void SetWinner()
    {
        winnerData.Value = playersDatas.OrderByDescending(pd => pd.PointsToAdd.Value).First();
    }

    bool EndedByTotalPlayerPointsLimit()
    {
        return playersDatas.Any(pd => pd.PointsToAdd.Value >= totalPlayerPointsLimit);
    }

    bool EndedByTotalPointsLimit()
    {
        return (playersDatas.Sum(pd => pd.PointsToAdd.Value)) >= totalPointsLimit;
    }

    public void DisposeAudio()
    {
        masterVolume.Dispose();
        musicVolume.Dispose();
        soundEffectsVolume.Dispose();
        interfaceVolume.Dispose();
    }
}

