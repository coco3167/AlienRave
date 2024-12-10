using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private GameObject optionsMenu;
        private UnityEvent optionsEvent;

        private void Start()
        {
            GetFocus();
            optionsEvent = optionsMenu.GetComponent<OptionsMenu>().backEvent;
        }

        private void OnEnable()
        {
            GetFocus();
        }

        public void PlayGame()
        {
            GameManager.Instance.Play();
            gameObject.SetActive(false);
        }

        public void OpenOptions()
        {
            optionsMenu.SetActive(true);
            optionsEvent.AddListener(GetFocus);
        }
    
        public void QuitGame()
        {
            Application.Quit();
        }

        private void GetFocus()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }
    }
}
