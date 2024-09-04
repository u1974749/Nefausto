using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject playPrefab;
    public GameObject textTouch;
    public Camera cam;
    public Collider floor;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("menuTheme");
        PlayerPrefs.SetFloat("PlayerX", 2.640977f);
        PlayerPrefs.SetFloat("PlayerY", -0.00999999f);
        PlayerPrefs.SetFloat("PlayerZ", 21.93249f);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touch = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6.3f));
            textTouch.SetActive(false);
            Instantiate(playPrefab, touch, Quaternion.identity);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("ExplainAnimation");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
