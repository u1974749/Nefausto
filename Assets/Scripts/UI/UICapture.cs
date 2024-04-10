using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICapture : MonoBehaviour
{
    [SerializeField] GameObject options;

    public void HelpOption()
    {
        SceneManager.LoadScene("Capture");
    }
    public void IgnoreOption()
    {
        options.SetActive(false);
    }
}
