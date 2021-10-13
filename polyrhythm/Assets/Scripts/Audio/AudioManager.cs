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
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Intro");
        callbackHandler = GetComponent<FMODCallbackHandler>();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Startup");
        GameManager.StartGame += BeginMusic;
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
        music.release();
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        system.release();
    }
}
