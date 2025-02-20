using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public Light2D globablLight;
    public GameObject globalLightPrefab;

    public static GameManager instance;

    [Header("Settings")]
    public GameObject settingsPage;
    [Range(0, 1)] public float lightStrength;
    [Range(0, 1)] public float volume;
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public Slider brightnessSlider;
    public TMP_Text brightnessText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            globablLight = Instantiate(globalLightPrefab, this.transform).GetComponent<Light2D>();
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Killing other Manager with Kindness");
        }
    }

    private void Start()
    {
        settingsPage.SetActive(false);
    }

    private void Update()
    {
        volume = volumeSlider.value / 100f;
        volumeText.text = volumeSlider.value.ToString();
        lightStrength = brightnessSlider.value / 100f;
        brightnessText.text = brightnessSlider.value.ToString();

        globablLight.intensity = lightStrength;
        soundManager.masterVolume = volume;
    }

    public void ShowHideSettings()
    {
        settingsPage.SetActive(!settingsPage.activeSelf);
    }
}
