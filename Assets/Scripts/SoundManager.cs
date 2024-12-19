using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Sound_State
{
    BUTTONCLICK,
    PIECEMOVED,
    PIECEDESTROYED,
    WIN,
    LOOSE
}
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource sfxAudioSource;
    public AudioClip buttonClick, pieceMove, pieceBreak, win, lose;
    public bool playSfx;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(Sound_State state)
    {
        if (!playSfx)
            return;
        switch (state)
        {
            case Sound_State.BUTTONCLICK:
                sfxAudioSource.PlayOneShot(buttonClick);
                break;
            case Sound_State.PIECEDESTROYED:
                sfxAudioSource.PlayOneShot(pieceBreak);
                break;
            case Sound_State.PIECEMOVED:
                sfxAudioSource.PlayOneShot(pieceMove);
                break;
            case Sound_State.LOOSE:
                sfxAudioSource.PlayOneShot(lose);
                break;
            case Sound_State.WIN:
                sfxAudioSource.PlayOneShot(win);
                break;
        }
    }
}
