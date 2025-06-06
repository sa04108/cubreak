using UnityEngine;

namespace Cubreak
{
    public class UIPanels : MonoBehaviour
    {
        public enum ENUM_UI_PANEL
        {
            Title,
            Stages,
            Option,
            InGame,
            GameClear,
            GameOver
        }

        [Header("Panels")]
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
            titlePanel.SetActive(panel.name == titlePanel.name);
            stagesPanel.SetActive(panel.name == stagesPanel.name);
            optionPanel.SetActive(panel.name == optionPanel.name);
            inGamePanel.SetActive(panel.name == inGamePanel.name);
            gameClearPanel.SetActive(panel.name == gameClearPanel.name);
            gameOverPanel.SetActive(panel.name == gameOverPanel.name);
        }
    } 
}
