using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedGameobject;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image backgroundImage;

    
        private Sprite[] UIAnimListLose, UIAnimListWin, UIAnimListMain;
        private int animIndex;

        private void Awake()
        {
            UIAnimListLose = Resources.LoadAll<Sprite>("LoseMenu_Anim");
            UIAnimListWin = Resources.LoadAll<Sprite>("WinMenu_Anim");
            UIAnimListMain = UIAnimListLose;
        }
        
        void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedGameobject);
            SetVictory();
        }
        
        private void FixedUpdate()
        {
            backgroundImage.sprite = UIAnimListMain[animIndex];
            animIndex++;
            animIndex %= UIAnimListMain.Length;
        }

        public void SetVictory()
        {
            if (GameManager.Instance.areUWinningSon)
            {
                titleText.text = "Victory";
                UIAnimListMain = UIAnimListWin;
            }
            else
            {
                titleText.text = "Game Over";
                UIAnimListMain = UIAnimListLose;
            }

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
