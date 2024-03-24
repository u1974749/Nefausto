using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] GameObject character;
        [SerializeField] GameObject dialogue;
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
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(architect.isBuilding)
                {
                    if(architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }

                if(n == lines.Length)
                {
                    character.SetActive(false);
                    dialogue.SetActive(false);
                    canvas.SetActive(false);
                    n = 0;
                }
                else
                {
                    Debug.Log("n " + n);
                    Debug.Log("nLines " + lines.Length);
                    architect.Build(lines[n]);
                    n++;
                }
            }
            //else if(Input.GetKeyDown(KeyCode.A))
              //  architect.Append(lina[Random.Range(0, lina.Length)]);
        }
    }
}
