using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Save 
{
    public List<Word> currentWords = new List<Word>();

    public int score = 0;
    public string player = "";
}
