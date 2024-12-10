using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseScript : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private GameObject optionsMenu;
        private UnityEvent optionsEvent;
        [SerializeField] private GameObject startMenu;
        private void Start()
        {
            GetFocus();
            optionsEvent = optionsMenu.GetComponent<OptionsMenu>().backEvent;
        }

        private void OnEnable()
        {
            GetFocus();
        }

        public void Back()
        {
            gameObject.SetActive(false);
        }

        public void Restart()
        {
            Debug.LogWarning("Restart not implemented");
        }

        public void Inputs()
        {
            Debug.LogWarning("Inputs display not implemented");
        }

        public void Options()
        {
            optionsMenu.SetActive(true);
            optionsEvent.AddListener(GetFocus);
        }

        public void StartMenu()
        {
            startMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void GetFocus()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }
    }
}
