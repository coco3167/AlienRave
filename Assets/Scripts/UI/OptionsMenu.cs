using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;

        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider voicesSlider;
        [SerializeField] private Slider SFXSlider;

        [HideInInspector] public UnityEvent backEvent;

        void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }

        public void Back()
        {
            backEvent.Invoke();
            backEvent.RemoveAllListeners();
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
    }
}
