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
        [SerializeField] private Image backgroundImage;

        private Sprite[] UIAnimList;
        private int animIndex;

        private void Awake()
        {
            UIAnimList = Resources.LoadAll<Sprite>("StartMenu_Anim");
        }

        private void OnEnable()
        {
            GetFocus();
        }

        private void FixedUpdate()
        {
            backgroundImage.sprite = UIAnimList[animIndex];
            animIndex++;
            animIndex %= UIAnimList.Length;
        }

        public void PlayGame()
        {
            if (GameManager.Instance.Play())
            {
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.uIClickButton, this.transform.position);
            }
            return;
            
            // Add animation when 2 players are not connected
        }

        public void OpenOptions()
        {
            optionsMenu.backEvent.AddListener(GetFocus);
            optionsMenu.gameObject.SetActive(true);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.greenPlayerAttack, this.transform.position);
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
