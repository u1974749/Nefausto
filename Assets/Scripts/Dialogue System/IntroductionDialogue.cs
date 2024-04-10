using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
public class IntroductionDialogue : MonoBehaviour {

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject character;
    [SerializeField] GameObject introduction;
    public void Active()
    {
        character.SetActive(true);
        introduction.SetActive(true);
        canvas.SetActive(true);
    }
}
