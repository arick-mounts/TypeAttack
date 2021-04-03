using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{

    public GameObject wordPrefab;
    public Transform wordCanvas;
    public GameManager wordManager;
    public float wordDelay = 1.5f;
    private float nextWordTime = 0f;

    public void Start()
    {
        wordDelay *= (GameManager.fallSpeed * .3f) + 1;
    }

    public WordDisplay SpawnWord(Word word)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-2.5f, 2.5f), 7f, 0);

        GameObject wordObj = Instantiate(wordPrefab, randomPosition, Quaternion.identity, wordCanvas);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();
        wordDisplay.SetWord(word);
        return wordDisplay;
    }

    public WordDisplay SpawnWord(Word word, float x, float y)
    {
        //Vector3 randomPosition = new Vector3(x, y, 0);


        GameObject wordObj = Instantiate(wordPrefab, wordCanvas);
        wordObj.GetComponent<RectTransform>().localPosition = new Vector2 (y,x) ;
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();
        wordDisplay.SetWord(word);
        return wordDisplay;
    }

    public void Update()
    {
        if (Time.time >= nextWordTime)
        {
            wordManager.AddWord();
            nextWordTime = Time.time + wordDelay;
            wordDelay *= .99f;
        }
    }
}
