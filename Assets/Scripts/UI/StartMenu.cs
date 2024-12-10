using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private GameObject optionsPrefab;

        private GameObject optionsMenu;
        private void Start()
        {
            optionsMenu = Instantiate(optionsPrefab);
            optionsMenu.SetActive(false);
            
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void OpenOptions()
        {
            optionsMenu.SetActive(true);
        }
    
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
