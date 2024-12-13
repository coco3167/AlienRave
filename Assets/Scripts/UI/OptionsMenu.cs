using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;

        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider ambSlider;
        [SerializeField] private Slider SFXSlider;
        [SerializeField] private Image backgroundImage;

        [HideInInspector] public UnityEvent backEvent;
        
        private Sprite[] UIAnimList;
        private int animIndex;

        private void Awake()
        {
            UIAnimList = Resources.LoadAll<Sprite>("PauseMenu_Anim");
        }

        void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }
        
        private void FixedUpdate()
        {
            backgroundImage.sprite = UIAnimList[animIndex];
            animIndex++;
            animIndex %= UIAnimList.Length;
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
