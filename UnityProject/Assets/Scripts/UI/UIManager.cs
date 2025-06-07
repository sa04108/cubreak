using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cubreak
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private UIPanels uiPanels;

        [Header("Title Buttons")]
        [SerializeField] private Button selectStageButton;
        [SerializeField] private Button exerciseButton;
        [SerializeField] private Button optionButton;

        [Header("Stage Manager")]
        [SerializeField] private StageManager stageManager;
        [SerializeField] private TMP_Text stageText;
        [SerializeField] private Button nextStageButton;

        private int score;
        [Header("Score Variables")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text finalScoreText;

        private void Start()
        {
            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.Title);

            selectStageButton.onClick.AddListener(() => uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.Stages));
            optionButton.onClick.AddListener(() => uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.Option));

#if DEBUG
            exerciseButton.onClick.AddListener(() => stageManager.StartStage(0));

#else
            exerciseButton.gameObject.SetActive(false);
#endif
        }

        public void ResetGame()
        {
            PlayerPrefs.DeleteAll();
            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.Title);
        }

        public void ScoreUp()
        {
            score++;
            scoreText.text = score.ToString();
        }

        public void SetStageUI(int stageNum)
        {
            score = 0;
            scoreText.text = "0";

            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.InGame);
            if (stageNum == 0)
            {
                scoreText.gameObject.SetActive(true);
                stageText.gameObject.SetActive(false);
                nextStageButton.gameObject.SetActive(false);
            }
            else
            {
                scoreText.gameObject.SetActive(false);
                stageText.gameObject.SetActive(true);
                stageText.text = "Stage " + stageNum;
                nextStageButton.gameObject.SetActive(true);
            }
        }

        public void EscapeStage()
        {
            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.Stages);
        }

        public void GoTitle()
        {
            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.Title);
        }

        public void GameClear()
        {
            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.GameClear, true);
            finalScoreText.text = scoreText.text;
            stageManager.ClearStage();
        }

        public void GameOver()
        {
            uiPanels.SetActivePanel(UIPanels.ENUM_UI_PANEL.GameOver, true);
            finalScoreText.text = scoreText.text;
        }
    } 
}
