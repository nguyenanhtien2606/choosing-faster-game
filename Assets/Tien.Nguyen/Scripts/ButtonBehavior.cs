using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] List<GameObject> spriteList;

    [SerializeField] int indexNumber;
    public int IndexNumber
    {
        get { return indexNumber; }
        set { indexNumber = value; }
    }

    private void Start()
    {
        gameManager = GameManager.GLOBAL;

        ChangePosCharacter();
    }

    public void ChangePosCharacter()
    {
        int numRan = Random.Range(0, spriteList.Count);

        while (gameManager.CharacterResignedList.Contains(numRan))
        {
            numRan = Random.Range(0, spriteList.Count);
        }

        IndexNumber = numRan;
        EnbleCharacterSprite(numRan);
        gameManager.RegisterCharacter(numRan);
    }

    void EnbleCharacterSprite(int charIndex)
    {
        //Set all character inactive
        foreach (var item in spriteList)
        {
            item.SetActive(false);
        }

        //active specific character
        spriteList[charIndex].SetActive(true);
    }
}
