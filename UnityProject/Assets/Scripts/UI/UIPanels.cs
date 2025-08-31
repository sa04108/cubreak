using UnityEngine;

namespace Cubreak
{
    public class UIPanels : MonoBehaviour
    {
        public enum ENUM_UI_PANEL
        {
            SignIn,
            SignUp,
            NewPassword,
            Title,
            Stages,
            Option,
            InGame,
            GameClear,
            GameOver
        }

        [Header("Backgrounds")]
        [SerializeField] private GameObject backgroundMain;
        [SerializeField] private GameObject backgroundEasyMode;
        [SerializeField] private GameObject backgroundHardMode;

        [Header("Panels")]
        [SerializeField] private GameObject signInPanel;
        [SerializeField] private GameObject signUpPanel;
        [SerializeField] private GameObject newPasswordPanel;
        [SerializeField] private GameObject titlePanel;
        [SerializeField] private GameObject stagesPanel;
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private GameObject inGamePanel;
        [SerializeField] private GameObject gameClearPanel;
        [SerializeField] private GameObject gameOverPanel;

        public void SetActivePanel(ENUM_UI_PANEL panel, bool additive = false)
        {
            GameObject panelObj = titlePanel;
            switch (panel)
            {
                case ENUM_UI_PANEL.SignIn:
                    panelObj = signInPanel;
                    break;
                case ENUM_UI_PANEL.SignUp:
                    panelObj = signUpPanel;
                    break;
                case ENUM_UI_PANEL.NewPassword:
                    panelObj = newPasswordPanel;
                    break;
                case ENUM_UI_PANEL.Title:
                    panelObj = titlePanel;
                    break;
                case ENUM_UI_PANEL.Stages:
                    panelObj = stagesPanel;
                    break;
                case ENUM_UI_PANEL.Option:
                    panelObj = optionPanel;
                    break;
                case ENUM_UI_PANEL.InGame:
                    panelObj = inGamePanel;
                    break;
                case ENUM_UI_PANEL.GameClear:
                    panelObj = gameClearPanel;
                    break;
                case ENUM_UI_PANEL.GameOver:
                    panelObj = gameOverPanel;
                    break;
            }

            if (additive)
            {
                panelObj.SetActive(true);
            }
            else
            {
                SetActivePanel(panelObj);
            }
        }

        public void SetActivePanel(GameObject panel)
        {
            signInPanel.SetActive(panel == signInPanel);
            signUpPanel.SetActive(panel == signUpPanel);
            newPasswordPanel.SetActive(panel == newPasswordPanel);
            titlePanel.SetActive(panel == titlePanel);
            stagesPanel.SetActive(panel == stagesPanel);
            optionPanel.SetActive(panel == optionPanel);
            inGamePanel.SetActive(panel == inGamePanel);
            gameClearPanel.SetActive(panel == gameClearPanel);
            gameOverPanel.SetActive(panel == gameOverPanel);

            backgroundMain.SetActive(panel != inGamePanel);
            backgroundEasyMode.SetActive(panel == inGamePanel);
        }
    } 
}
