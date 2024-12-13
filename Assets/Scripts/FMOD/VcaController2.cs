using UnityEngine;
using UnityEngine.UI;

public class VcaController2 : MonoBehaviour
{
    private FMOD.Studio.VCA VcaController;
    public string VcaName;
    
    private Slider volumeSlider;
    void Start()
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        Debug.Log(VcaController);
        volumeSlider = GetComponent<Slider>();
    }

    public void SetVolume(float volume)
    {
        VcaController.setVolume(volume);
        Debug.Log(volume);
    }
}