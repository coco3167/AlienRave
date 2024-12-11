using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private Image backgroundImage;

        private Sprite[] UI_AnimList;
        private int animIndex;

        private void Awake()
        {
            UI_AnimList = Resources.LoadAll<Sprite>("UI_Anim");
        }

        private void OnEnable()
        {
            GetFocus();
        }

        private void FixedUpdate()
        {
            backgroundImage.sprite = UI_AnimList[animIndex];
            animIndex++;
            animIndex %= UI_AnimList.Length;
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
