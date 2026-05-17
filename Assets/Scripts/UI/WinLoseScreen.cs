using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CoreBreach.GameState;

namespace CoreBreach.UI
{
    public class WinLoseScreen : MonoBehaviour
    {
        [SerializeField] private GameStateController gameStateController;
        [SerializeField] private GameObject rootPanel;
        [SerializeField] private TMP_Text   resultLabel;
        [SerializeField] private Button     restartButton;
        [Header("Result Text")]
        [SerializeField] private string winText  = "VICTORY";
        [SerializeField] private string loseText = "CORE DESTROYED";
        [SerializeField] private Color  winColor  = new Color(0.4f, 1f, 0.4f);
        [SerializeField] private Color  loseColor = new Color(1f, 0.3f, 0.3f);

        private void Awake()
        {
            if (rootPanel != null) rootPanel.SetActive(false);
            if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        }

        private void OnEnable()
        {
            if (gameStateController == null) return;
            gameStateController.OnGameWon  += HandleWin;
            gameStateController.OnGameLost += HandleLose;
        }

        private void OnDisable()
        {
            if (gameStateController == null) return;
            gameStateController.OnGameWon  -= HandleWin;
            gameStateController.OnGameLost -= HandleLose;
        }

        private void HandleWin()  => Show(winText,  winColor);
        private void HandleLose() => Show(loseText, loseColor);

        private void Show(string text, Color color)
        {
            if (rootPanel != null) rootPanel.SetActive(true);
            if (resultLabel != null)
            {
                resultLabel.text  = text;
                resultLabel.color = color;
            }
        }

        private void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}