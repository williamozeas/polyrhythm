using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager i;

    private FMOD.Studio.EventInstance music;
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
        music = FMODUnity.RuntimeManager.CreateInstance("event:/MainMusic");
        callbackHandler = GetComponent<FMODCallbackHandler>();
        BeginMusic();
    }

    void BeginMusic() {
        callbackHandler.Begin(music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
