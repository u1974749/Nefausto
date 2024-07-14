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
        [SerializeField] TextMeshProUGUI newName;
        [SerializeField] GameObject dialogue;
        [SerializeField] GameObject options;
        bool activeOptions = false;
        private int n = 0;
        // Start is called before the first frame update
        /*string[] lines = new string[5]
        {
            "The criature..."
        };*/
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.speed = 0.5f;
            newName.text = gameObject.name;
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
                    architect.ForceComplete();
                }
                else if (n == lines.Length)
                {
                    if (activeOptions)
                    {
                        options.SetActive(true);
                        n = lines.Length;
                    }
                    else
                    {
                        character.SetActive(false);
                        dialogue.SetActive(false);
                        canvas.SetActive(false);
                        controls.SetActive(true);
                        n = 0;
                    }
                }
                else
                {    
                    architect.Build(lines[n]);
                    n++;
                }
            }
            //else if(Input.GetKeyDown(KeyCode.A))
              //  architect.Append(lina[Random.Range(0, lina.Length)]);
        }

        public void onOptions()
        {
            activeOptions = true;
        }

        public void offOptions()
        {
            activeOptions = false;
            character.SetActive(false);
            dialogue.SetActive(false);
            canvas.SetActive(false);
            controls.SetActive(true);
            //n = 0;
        }
    }
}
