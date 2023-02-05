using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Dropdown qualityDropdown;
    
    public bool initialized = false;
    public Slider mouseSensitivitySlider;

    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeTextUI = null;

    Resolution[] resolutions;

    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
        LoadVolumeValues();
        LoadMouseSensitivity();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetMouseSensitivity(float val) 
    {
        if (!initialized) return;
        if (!Application.isPlaying) return;
        PlayerPrefs.SetFloat("Sensitivity", val);
    }

    public void LoadMouseSensitivity() 
    {
        if (PlayerPrefs.HasKey("Sensitivity")) 
        {
            mouseSensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
        initialized = true;
    }

    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString("0");
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadVolumeValues();
    }

    void LoadVolumeValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        //Quality
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;
        //Resolution
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = currentResolutionIndex;
        //FullScreen
        if (PlayerPrefs.HasKey("FullScreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference"));
        else
            Screen.fullScreen = true;
    }
}
