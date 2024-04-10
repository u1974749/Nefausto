using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace TESTING
{
    public class Test_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;
        [SerializeField] public string[] lines;
        [SerializeField] GameObject canvas;
        [SerializeField] GameObject controls;
        [SerializeField] GameObject character;
        [SerializeField] TextMeshProUGUI name;
        [SerializeField] GameObject dialogue;
        [SerializeField] GameObject options;
        bool activeOptions = false;
        private int n = 0;
        // Start is called before the first frame update
        /*string[] lines = new string[5]
        {
            "Hi, Mencía! Do you want some information about criatures?",
            "The criature...",
            "I think it's a white cat with a horn.",
            "If you see, the cat have the tongue out.",
            "Sound crazy! The Gatipedro steal the offering."
        };*/
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.speed = 0.5f;
            name.text = "Nefausto";
            n=0;
            architect.Build(lines[n]);
            n++;
        }

        // Update is called once per frame
        void Update()
        {
            controls.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {

                if(architect.checkBuild)
                {
                    Debug.Log("Building");
                    architect.ForceComplete();
                }
                if (n == lines.Length)
                {
                    if (activeOptions)
                    {
                        options.SetActive(true);
                        activeOptions = false;
                    }
                    else
                    {
                        character.SetActive(false);
                        dialogue.SetActive(false);
                        canvas.SetActive(false);
                        controls.SetActive(true);
                    }
                    n = 0;

                }
                else
                {    
                    Debug.Log("n " + n);
                    architect.Build(lines[n]);
                    n++;
                }
            }
            //else if(Input.GetKeyDown(KeyCode.A))
              //  architect.Append(lina[Random.Range(0, lina.Length)]);
        }

        public void changeOptions()
        {
            activeOptions = true;
        }
    }
}
