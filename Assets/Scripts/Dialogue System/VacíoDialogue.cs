using System.Collections;
using System.Collections.Generic;
using TESTING;
using UnityEngine;

public class VacíoDialogue : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject character;
    [SerializeField] GameObject introduction;
    [SerializeField] GameObject finalMessage;
    public void ActiveIntroduction()
    {
        character.SetActive(true);
        introduction.SetActive(true);
        canvas.SetActive(true);
        introduction.GetComponent<Test_Architect>().changeOptions();
    }

    public void ActiveFinalMessage()
    {
        character.SetActive(true);
        finalMessage.SetActive(true);
        canvas.SetActive(true);
    }
}
