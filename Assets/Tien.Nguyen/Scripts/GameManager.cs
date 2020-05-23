using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GLOBAL;

    AudioManager audioManager;

    [Header("Button Elements Setting")]
    [SerializeField] List<GameObject> buttonCharacterList;

    [Header("Core Game Setting")]
    [SerializeField] float timeOfLevel;
    [SerializeField] Text timeText;
    [SerializeField] GameObject questionUIContentParent;
    [SerializeField] int score = 100;
    [SerializeField] UIBehavior ui_Behavior;
    

    private int playerScore;
    private int levelCount;

    private float orginTimeOfLevel;

    //Gameplay variables
    List<int> listIndexChar = new List<int>();
    List<int> askListIndex = new List<int>();

    private int indexCurrentNeedSelectInListAsk;
    private int indexCurrentNeedSelect;

    private int level = 3;

    public List<int> CharacterResignedList { get; set; } = new List<int>();

    private void Awake()
    {
        Time.timeScale = 1;
        GLOBAL = this;
    }

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();

        orginTimeOfLevel = timeOfLevel;

        IncreasePoint(0);

        RenderAks();
    }

    public void RegisterCharacter(int charIndex)
    {
        CharacterResignedList.Add(charIndex);
    }

    void RenderAks()
    {
        //Create a list int from 0 -> 25 (index of 26 character in alphabet)
        listIndexChar.Clear();
        for (int i = 0; i < 26; i++)
        {
            listIndexChar.Add(i);
        }

        //Reset ask data
        askListIndex.Clear();

        foreach (Transform item in questionUIContentParent.transform)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < level; i++)
        {
            Instantiate(ui_Behavior.QuesionCell, questionUIContentParent.transform);
        }

        //Random ask data
        for (int i = 0; i < level; i++)
        {
            int numRan = Random.Range(0, listIndexChar.Count);
            listIndexChar.Remove(numRan);

            askListIndex.Add(numRan);

            StartCoroutine(DisplayQuestion(i, numRan));
        }

        indexCurrentNeedSelectInListAsk = 0;
        indexCurrentNeedSelect = askListIndex[indexCurrentNeedSelectInListAsk];
    }

    IEnumerator DisplayQuestion(int indexChild, int indexChar)
    {
        yield return new WaitForSeconds(0.1f);
        questionUIContentParent.transform.GetChild(indexChild).GetComponent<AskUI>().DisplayUI(indexChar);
    }

    public void SelectAnswer(int indexChar)
    {
        if (indexChar >= 0)
        {
            if (indexChar == indexCurrentNeedSelect)
            {
                if (indexCurrentNeedSelectInListAsk < askListIndex.Count - 1)
                {
                    questionUIContentParent.transform.GetChild(indexCurrentNeedSelectInListAsk).GetComponent<AskUI>().CorrentEffect();
                    IncreasePoint(score);

                    indexCurrentNeedSelectInListAsk++;
                    indexCurrentNeedSelect = askListIndex[indexCurrentNeedSelectInListAsk];

                    audioManager.PlayConrrentAudio();
                }
                else
                {
                    Debug.Log("end game level");

                    //Continue render ask
                    ChangePositionCharacters();
                    RenderAks();

                    levelCount++;
                    if (levelCount < 2)
                    {
                        if (level < 7)
                        {
                            level++;
                        }

                        levelCount = 0;
                    }
                }
            }
            else
            {
                audioManager.PlayWrongAudio();
            }
        }
    }

    private void Update()
    {
        if (timeOfLevel > 0)
        {
            timeOfLevel -= Time.deltaTime;
            timeText.text = Mathf.Round(timeOfLevel).ToString();
        }
        else
        {
            ui_Behavior.EndgamePanel.SetActive(true);
            ui_Behavior.ScoreTextEndGame.text = playerScore.ToString();
            Time.timeScale = 0;
            
            return;
        }
    }

    void ChangePositionCharacters()
    {
        CharacterResignedList.Clear();
        foreach (var item in buttonCharacterList)
        {
            item.GetComponent<ButtonBehavior>().ChangePosCharacter();
        }
    }

    void IncreasePoint(int point)
    {
        playerScore += point;
        ui_Behavior.ScoreText.text = playerScore.ToString();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
