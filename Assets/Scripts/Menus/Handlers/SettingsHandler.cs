using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public Sprite toggleOnImage, toggleOffImage;

    public Image musicToggleImage, sfxToggleImage, NotificationsToggleImage;

    public bool currentMusicState, currentSFXState, currentNotificationsState;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        initAllSettings();
    }

    void initAllSettings()
    {
        currentMusicState = true;
        currentNotificationsState = true;
        currentSFXState = true;
        musicToggleImage.sprite = toggleOnImage;
        sfxToggleImage.sprite = toggleOnImage;
        NotificationsToggleImage.sprite = toggleOnImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickBack()
    {
        SoundManager.Instance.playSound(Sound_State.BUTTONCLICK);
        SceneManager.Instance.switchState(GameState.MAINMENU);
    }

    public void onToggleMusic()
    {
        if (currentMusicState)
        {
            currentMusicState = false;
            musicToggleImage.sprite = toggleOffImage;
        }
        else
        {
            currentMusicState = true;
            musicToggleImage.sprite = toggleOnImage;
        }
    }

    public void onToggleSFX()
    {
        if (currentSFXState)
        {
            currentSFXState = false;
            sfxToggleImage.sprite = toggleOffImage;
            SoundManager.Instance.playSfx = false;
        }
        else
        {
            currentSFXState = true;
            sfxToggleImage.sprite = toggleOnImage;
            SoundManager.Instance.playSfx = true;
        }
    }

    public void onToggleNotifications()
    {
        if (currentNotificationsState)
        {
            currentNotificationsState = false;
            NotificationsToggleImage.sprite = toggleOffImage;
        }
        else 
        {
            currentNotificationsState = true;
            NotificationsToggleImage.sprite = toggleOnImage;
        }
    }

    public void onClickPrivacyPolicy()
    { }
}
