using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{

    public GameManager gameManager;

    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            gameManager.TypeLetter(letter);
        }
    }
}
