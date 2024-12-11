using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private OptionsMenu optionsMenu;
        [SerializeField] private List<Image> playerJoinImages;
        
        private void OnEnable()
        {
            GetFocus();
        }

        public void PlayGame()
        {
            if(GameManager.Instance.Play())
                return;
            
            // Add animation when 2 players are not connected
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

        public void OnPlayerJoined(PlayerInput playerInput)
        {
            playerJoinImages[playerInput.playerIndex].enabled = true;
        }

        private void GetFocus()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }
    }
}
