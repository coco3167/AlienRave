using UnityEngine;

public class FMODVCAs : MonoBehaviour
{
    private FMOD.Studio.VCA masterVCA;
    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA ambVCA;
    void Start()
    {
        masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        ambVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Amb");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
