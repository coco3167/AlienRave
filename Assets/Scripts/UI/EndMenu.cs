using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject mainMenu;
    
        void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
        }

        public void SetVictory(bool isVictory, int score)
        {
            if(isVictory)
                titleText.text = "Victoire";
            else
                titleText.text = "Game Over";
            
            scoreText.text = $"Score : {score}";
        }

        public void Restart()
        {
            Debug.LogWarning("No restart implemented");
        }

        public void MainMenu()
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
