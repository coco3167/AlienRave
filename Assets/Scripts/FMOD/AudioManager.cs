using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using JetBrains.Annotations;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    private void Awake()
    {
        Instance = this;
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound,worldPos);
    }
    
    public EventInstance musicInstance;
    public EventInstance ambInstance;
    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(FMODEvents.Instance.music);
        musicInstance.start();
        ambInstance = RuntimeManager.CreateInstance(FMODEvents.Instance.crowd2D);
        ambInstance.start();
    }
    public void SetMusicParameter(string parameterName, string parameterLabel)
    {
        musicInstance.setParameterByNameWithLabel(parameterName, parameterLabel);
    }
    public void SetAmbParameter(string parameterName, string parameterLabel)
    {
        ambInstance.setParameterByNameWithLabel(parameterName, parameterLabel);
    }
    
}
