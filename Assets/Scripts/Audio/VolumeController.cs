using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController:MonoBehaviour
{
    [SerializeField] List<Slider> volumeSliders;
    [SerializeField] List<TextMeshProUGUI> volumeTexts;
    [SerializeField] MatchSO matchData;
    bool volumeSlidersSetted;

    private void Awake()
    {
        SetInitialVolumesSliders();        
    }

    private void OnEnable()
    {
        matchData.SetAudioMixer();
    }

    private void OnDisable()
    {
        matchData.DisposeAudio();
    }

    public void SetAllVolumesUIs()
    {
        if (!volumeSlidersSetted) return;
        matchData.masterVolume.Value = volumeSliders[0].value;
        volumeTexts[0].text = volumeSliders[0].value.ToString("f0");
        matchData.soundEffectsVolume.Value = volumeSliders[1].value;
        volumeTexts[1].text = volumeSliders[1].value.ToString("f0");
        matchData.interfaceVolume.Value = volumeSliders[2].value;
        volumeTexts[2].text = volumeSliders[2].value.ToString("f0");
        matchData.musicVolume.Value = volumeSliders[3].value;
        volumeTexts[3].text = volumeSliders[3].value.ToString("f0");
    }
    void SetInitialVolumesSliders()
    {
        volumeSliders[0].value = matchData.masterVolume.Value;
        volumeTexts[0].text = volumeSliders[0].value.ToString("f0");
        volumeSliders[1].value = matchData.soundEffectsVolume.Value;
        volumeTexts[1].text = volumeSliders[1].value.ToString("f0");
        volumeSliders[2].value = matchData.interfaceVolume.Value;
        volumeTexts[2].text = volumeSliders[2].value.ToString("f0");
        volumeSliders[3].value = matchData.musicVolume.Value;
        volumeTexts[3].text = volumeSliders[3].value.ToString("f0");
        volumeSlidersSetted = true;
    }

}







