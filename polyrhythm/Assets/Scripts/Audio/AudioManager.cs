using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.System system;
    public static AudioManager i;

    public FMOD.Studio.EventInstance music;
    private FMODCallbackHandler callbackHandler;
    FMOD.Studio.EventInstance PauseSnapshot;

    void Awake()
    {
        if(i != null)
            GameObject.Destroy(i);
        else {
            i = this;
        }

        DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        callbackHandler = GetComponent<FMODCallbackHandler>();
        GameManager.StartGame += BeginMusic;
        GameManager.MainMenu += StopMusic;
        GameManager.MainMenu += Startup;
        GameManager.Pause += OnPause;
        GameManager.Unpause += OnUnpause;
        Startup();
    }

    public void BeginMusic() {
        callbackHandler.Begin(music);
    }

    public void AdvanceMusic(int stage) {
        music.release();
        switch(stage) {
            case 1:
                music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/First");
                BeginMusic();
                break;
        }
    }

    public void StopMusic() {
        if(music.isValid()) {
            music.release();
            music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void OnPause() {
        if(GameManager.i.IsLoop == 0) {
            StartCoroutine(OnPauseNoLoop());
        } else {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Paused", 1);
        }
    }

    public void OnUnpause() {
        if(GameManager.i.IsLoop == 0) {
            OnUnpauseNoLoop();
        } else {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Paused", 0);
        }
    }

    void OnDestroy() {
        system.release();
    }

    public void Startup() {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Intro");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Startup");
    }


    IEnumerator OnPauseNoLoop() {
        PauseSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/PauseMusic");
        PauseSnapshot.start();
        yield return new WaitForSeconds(2f);
        music.setPaused(true);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Paused", 1);
    }

    void OnUnpauseNoLoop() {
        music.setPaused(false);
        PauseSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Paused", 0);
    }

    public void UpdateVolume(float vol) {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Volume", vol);
    }
}
