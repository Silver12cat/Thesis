using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    public ResItem[] resolutions;
    private int selectedResolution;
    public Text resolutionLabel;
    public AudioMixer theMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterLabel, musicLabel, sfxLabel;
    public AudioSource sfxloop;
    // Start is called before the first frame update
    void Start()
    {
        //checking if the full screen is toggled
        fullscreenTog.isOn = Screen.fullScreen;

        //checking if vSync is toggled
        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }
        //search for resolution in list
        bool foundRes = false;
        for(int i=0; i <resolutions.Length; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResolution();
            }
        }
        //if resolution is not supported, shows the screens resolution instead
        if (!foundRes)
        {
            resolutionLabel.text =Screen.width.ToString() + " x " + Screen.height.ToString();
        }
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }
        masterLabel.text = (masterSlider.value + 80).ToString();
        musicLabel.text = (musicSlider.value + 80).ToString();
        sfxLabel.text = (sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //moves the resolution display one to left
    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResolution();
    }

    //moves the resolution display one to right
    public void ResRight()
    {
        selectedResolution++;
        if(selectedResolution >resolutions.Length - 1)
        {
            selectedResolution = resolutions.Length - 1;
        }
        UpdateResolution();
    }

    //displays the current resolution
    public void UpdateResolution()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        //apply fullscreen
        //Screen.fullScreen = fullscreenTog.isOn;

        //apply vsync
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        //set resolution and fullscreen

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVol()
    {
        //changing the Master volume settings
        masterLabel.text = (masterSlider.value + 80).ToString();
        theMixer.SetFloat("MasterVol", masterSlider.value);
        //saving the Master volume settings
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicVol()
    {
        //changing the Music volume settings
        musicLabel.text = (musicSlider.value + 80).ToString();
        theMixer.SetFloat("MusicVol", musicSlider.value);
        //saving the music volume settings
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVol()
    {
        //changing the SFX volume settings
        sfxLabel.text = (sfxSlider.value + 80).ToString();
        theMixer.SetFloat("SFXVol", sfxSlider.value);
        //saving the SFX volume settings
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }

    public void PlaySFXLoop()
    {
        sfxloop.Play();
    }

    public void StopSFXLoop()
    {
        sfxloop.Stop();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;

}