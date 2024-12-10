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
            GameManager.Instance.Play();
            gameObject.SetActive(false);
        }

        public void Restart()
        {
            GameManager.Instance.Restart(false);
            Debug.LogWarning("Restart not implemented");
        }

        public void Options()
        {
            optionsMenu.SetActive(true);
            optionsEvent.AddListener(GetFocus);
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
