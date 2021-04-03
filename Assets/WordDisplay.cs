using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WordDisplay : MonoBehaviour
{

    public Text text;
    public Word word;

    public RectTransform rt ;

    public void SetWord (Word _word)
    {
        word = _word;
        rt =  GetComponent<RectTransform>();
        word.height = rt.anchoredPosition.y;
        word.horiz = rt.anchoredPosition.x;
        text.text = word.word;
    }


    public void RemoveLetter()
    {
        text.text = text.text.Remove(0,1);
        text.color = Color.red;
    }

    public void RemoveWord()
    {
        FindObjectOfType<GameManager>().UpdateScore();
        Destroy(gameObject);
    }
    void Update()
    {

        transform.Translate(0f, -GameManager.fallSpeed * Time.deltaTime, 0f);
        if (rt != null)
        word.height = rt.anchoredPosition.y;

        if (word.RemoveLetterBool)
        {
            RemoveLetter();
            word.RemoveLetterBool = false;
        }
        if (word.DeleteLetterBool)
        {
            RemoveWord();
            word.DeleteLetterBool = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ScreenBottom")
        {
            Debug.Log("Game Over Man");
            Destroy(gameObject);
            FindObjectOfType<GameManager>().NextScene() ;
        }
    }
}
