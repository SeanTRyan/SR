using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer m_audioMixer = null;

    [SerializeField] private Slider m_masterVolumeSlider = null;

    [SerializeField] private Text m_musicVolumeText = null;
    [SerializeField] private Slider m_musicVolumeSlider = null;

    [SerializeField] private Dropdown m_resolutionDropdown = null;
    [SerializeField] private Dropdown m_qualityDropdown = null;
    [SerializeField] private Dropdown m_vSyncDropdown = null;
    [SerializeField] private Dropdown m_antialiasingDropdown = null;

    [SerializeField] private Toggle m_fullscreenToggle = null;

    private Resolution[] m_resolutions;

    private void Awake()
    {
        InitialiseResolutions();

        InitialiseListeners();
    }

    private void InitialiseListeners()
    {
        m_fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        m_resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        m_qualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        m_vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        m_antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        m_musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicSliderChange(); });
    }

    private void InitialiseResolutions()
    {
        m_resolutions = Screen.resolutions;

        m_resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < m_resolutions.Length; i++)
        {
            string option = m_resolutions[i].width + " x " + m_resolutions[i].height;
            options.Add(option);
        }

        m_resolutionDropdown.AddOptions(options);
        m_resolutionDropdown.RefreshShownValue();
    }

    public void OnResolutionChange()
    {
        Resolution resolution = m_resolutions[m_resolutionDropdown.value];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.SetQualityLevel(m_qualityDropdown.value);
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2f, m_antialiasingDropdown.value);
    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = m_vSyncDropdown.value;
    }

    public void OnMusicSliderChange()
    {
        m_musicVolumeText.text = string.Format("{0:0}", m_musicVolumeSlider.value);
    }

    public void OnFullScreenToggle()
    {
        Screen.fullScreen = (m_fullscreenToggle.isOn);
        Screen.fullScreenMode = FullScreenMode.MaximizedWindow;

        Screen.SetResolution(Screen.width, Screen.height, m_fullscreenToggle.isOn);

        Debug.Log("SCREEN MODE " + Screen.fullScreenMode);
        Debug.Log("FULL SCREEN " + Screen.fullScreen);
    }
}
