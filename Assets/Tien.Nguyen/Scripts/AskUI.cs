using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AskUI : MonoBehaviour
{
    [SerializeField] List<Image> imageCharacterList;
    int indexChar;

    public void DisplayUI(int index)
    {
        foreach (var item in imageCharacterList)
        {
            item.gameObject.SetActive(false);
        }

        imageCharacterList[index].gameObject.SetActive(true);
        indexChar = index;
    }

    public void CorrentEffect()
    {
        imageCharacterList[indexChar].GetComponent<Image>().color = Color.yellow;
    }
}
