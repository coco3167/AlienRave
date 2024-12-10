using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private OptionsMenu optionsMenu;
        
        private void OnEnable()
        {
            GetFocus();
        }

        public void PlayGame()
        {
            GameManager.Instance.Play();
        }

        public void OpenOptions()
        {
            optionsMenu.backEvent.AddListener(GetFocus);
            optionsMenu.gameObject.SetActive(true);
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
