using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;

    public FMOD.Studio.EventInstance music;
    private FMODCallbackHandler callbackHandler;

    void Awake()
    {
        if(i != null)
            GameObject.Destroy(i);
        else
            i = this;

        DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Intro");
        callbackHandler = GetComponent<FMODCallbackHandler>();
        BeginMusic();
    }

    public void BeginMusic() {
        callbackHandler.Begin(music);
    }

    public void StopMusic() {
        music.release();
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
