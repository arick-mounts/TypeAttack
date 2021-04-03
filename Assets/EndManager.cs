using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndManager : MonoBehaviour
{
    bool isHighscores = false;

    public GameObject Panel;

    public void ReturnToMenu()
    {
        GameManager.score = 0;
        GameManager.player = "PlayerName";

        
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Debug.Log("Ended Game");
        Application.Quit();
    }

    public void ToggleHS()
    {
        if (!isHighscores)
        {
            Panel.SetActive(true);
            isHighscores= true;
        }
        else
        {
            Panel.SetActive(false);
            isHighscores = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
