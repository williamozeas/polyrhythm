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
            FMOD.RESULT result = FMOD.Studio.System.create(out system);
            
            if (result != FMOD.RESULT.OK)
            {
                UnityEngine.Debug.Log("NOT OK");
            } else {
                UnityEngine.Debug.Log("OK");
            }

            FMOD.System sys;
            //system.getLowLevelSystem
            ADVANCEDSETTINGS settings = new ADVANCEDSETTINGS();
            system.getCoreSystem(out sys);
            sys.getAdvancedSettings(ref settings);
            settings.resamplerMethod = DSP_RESAMPLER.NOINTERP;
            sys.setAdvancedSettings(ref settings);
            System.IntPtr extrasettings = new System.IntPtr(0);
            result = system.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, extrasettings);
            if (result != FMOD.RESULT.OK)
            {
                UnityEngine.Debug.Log("NOT OK2");
            } else {
                UnityEngine.Debug.Log("OK2");
            }
            
            /*
            FMOD.System realSys;
            FMOD.ADVANCEDSETTINGS advancedSettings = new ADVANCEDSETTINGS();
            //FMODUnity.RuntimeManager
            FMODUnity.RuntimeManager.StudioSystem.getCoreSystem(out realSys); 
            realSys.getAdvancedSettings(ref advancedSettings);
            advancedSettings.resamplerMethod = DSP_RESAMPLER.SPLINE;
            realSys.setAdvancedSettings(ref advancedSettings);
            //result = FMODUnity.RuntimeManager.StudioSystem.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, extrasettings);
            
            UnityEngine.Debug.Log(advancedSettings.resamplerMethod);
            if (result != FMOD.RESULT.OK)
            {
                UnityEngine.Debug.Log("NOT OK2");
            } else {
                UnityEngine.Debug.Log("OK2");
            }
            */

            FMOD.System realSys2;
            FMOD.ADVANCEDSETTINGS advancedSettings2 = new ADVANCEDSETTINGS();
            //FMODUnity.RuntimeManager
            FMODUnity.RuntimeManager.StudioSystem.getCoreSystem(out realSys2); 
            realSys2.getAdvancedSettings(ref advancedSettings2);
            
            UnityEngine.Debug.Log(advancedSettings2.resamplerMethod);
            i = this;
        }

        DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        

        music = FMODUnity.RuntimeManager.CreateInstance("event:/Quality Test");
        callbackHandler = GetComponent<FMODCallbackHandler>();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Startup");
        GameManager.StartGame += BeginMusic;
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

    void OnDestroy() {
        system.release();
    }
}
