using UnityEngine;
using FMODUnity;
public class AudioManager : MonoBehaviour
{
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound,worldPos);
    }
    public static AudioManager instance { get; private set;}

    private void Awake()
    {
        instance = this;
    }
}
