using System.Linq;
using UniRx;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] MatchSO matchData;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource interfaceSounds;
    CompositeDisposable disposables;

    private void Start()
    {
        matchData.SetAudioMixer();
        // update subscription
        var update = Observable.EveryUpdate();
        disposables = new CompositeDisposable(
            update
            .Where(_ => MusicEnd())
            .Subscribe(_ => PlayNewTrack()));       
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }

   
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void PlayInterfaceClip(AudioClip audioClip) =>
        interfaceSounds.PlayOneShot(audioClip);   
    
    void PlayNewTrack()
    {
        musicSource.clip = GetRandomMusic();
        musicSource.Play();
    }
    bool MusicEnd() =>
        !musicSource.isPlaying;

    AudioClip GetRandomMusic() =>
            matchData.actualMusicToPlay[Random.Range(0, matchData.actualMusicToPlay.Count)];


}




