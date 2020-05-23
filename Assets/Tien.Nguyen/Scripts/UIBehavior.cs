using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    [Header("Question UI Controller")]
    [SerializeField] GameObject uiQuestion;
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject quesionCell;
    [SerializeField] Vector3 offset;

    [Header("Score UI")]
    [SerializeField] Text scoreText;
    [SerializeField] Text scoreTextEndGame;

    [Header("End game panel")]
    [SerializeField] GameObject endgamePanel;

    [Header("Setting panel")]
    [SerializeField] GameObject settingPanel;
    [SerializeField] Dropdown dropdownQuality;

    AudioSource audioSource;
    [SerializeField] Slider sliderAudio;

    [Header("Tutorial Panel")]
    [SerializeField] GameObject tutorialPanel;

    public Text ScoreText
    {
        get { return scoreText; }
        set { scoreText = value; }
    }

    public GameObject QuesionCell
    {
        get { return quesionCell; }
        set { quesionCell = value; }
    }

    public GameObject EndgamePanel
    {
        get { return endgamePanel; }
        set { endgamePanel = value; }
    }

    public Text ScoreTextEndGame
    {
        get { return scoreTextEndGame; }
        set { scoreTextEndGame = value; }
    }

    private void Start()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0;

        endgamePanel.SetActive(false);
        settingPanel.SetActive(false);

        audioSource = GameManager.GLOBAL.GetComponent<AudioSource>();

        dropdownQuality.value = QualitySettings.GetQualityLevel();
        sliderAudio.value = audioSource.volume;
    }

    private void Update()
    {
        uiQuestion.transform.position = Camera.main.WorldToScreenPoint(thePlayer.transform.position + offset);
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetQuaility()
    {
        QualitySettings.SetQualityLevel(dropdownQuality.value, true);
    }

    public void AudioControl()
    {
        audioSource.volume = sliderAudio.value;
    }

    public void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
