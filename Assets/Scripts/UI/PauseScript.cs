using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PauseScript : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject controlsMenu;
        [SerializeField] private GameObject controlsBackButton;
        [SerializeField] private Image backgroundImage;
        
        private UnityEvent optionsEvent;
        
        private Sprite[] UIAnimList;
        private int animIndex;

        private void Awake()
        {
            UIAnimList = Resources.LoadAll<Sprite>("PauseMenu_Anim");
        }
        
        private void Start()
        {
            GetFocus();
            optionsEvent = optionsMenu.GetComponent<OptionsMenu>().backEvent;
        }
        
        private void FixedUpdate()
        {
            backgroundImage.sprite = UIAnimList[animIndex];
            animIndex++;
            animIndex %= UIAnimList.Length;
        }

        private void OnEnable()
        {
            GetFocus();
        }

        public void Back()
        {
            GameManager.Instance.Play();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.uIOnButton, this.transform.position);
            //gameObject.SetActive(false);
        }

        public void Restart()
        {
            GameManager.Instance.Restart(false);
        }

        public void Options()
        {
            optionsMenu.SetActive(true);
            optionsEvent.AddListener(GetFocus);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.uIOnButton, this.transform.position);
        }

        public void Controls()
        {
            TutoManager.Instance.LaunchTuto(true);
            gameObject.SetActive(false);
            //controlsMenu.SetActive(true);
            //EventSystem.current.SetSelectedGameObject(controlsBackButton);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.uIOnButton, this.transform.position);
        }

        public void BackControls()
        {
            controlsMenu.SetActive(false);
            GetFocus();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.uIOnButton, this.transform.position);
        }

        public void StartMenu()
        {
            GameManager.Instance.Restart(true);
        }

        private void GetFocus()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }
    }
}
