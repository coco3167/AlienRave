using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedGameobject;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider voicesSlider;
    [SerializeField] private Slider SFXSlider;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

    public void OnMusicVolumeChanged()
    {
        Debug.LogWarning("Music volume change not implemented");
    }
    
    public void OnVoicesVolumeChanged()
    {
        Debug.LogWarning("Voices volume change not implemented");
    }
    
    public void OnSFXVolumeChanged()
    {
        Debug.LogWarning("SFX volume change not implemented");
    }

    public void ChangeInput()
    {
        Debug.LogWarning("Input change not implemented");
    }
}
