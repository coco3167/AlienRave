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
    
        void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
            SetVictory();
        }

        public void SetVictory()
        {
            if(GameManager.Instance.areUWinningSon)
                titleText.text = "Victory";
            else
                titleText.text = "Game Over";
            
            scoreText.text = $"Score : {GameManager.Instance.GetFinalScore()}";
        }

        public void Restart()
        {
            GameManager.Instance.Restart(false);
        }

        public void MainMenu()
        {
            GameManager.Instance.Restart(true);
            gameObject.SetActive(false);
        }
    }
}
