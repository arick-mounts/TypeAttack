using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word 
{

    public string word;
    private int typeIndex;

    public float height = 0;
    public float horiz = 0;

    public bool RemoveLetterBool = false;
    public bool DeleteLetterBool = false;
    //private WordDisplay display;

    public Word(string _word/*, WordDisplay _display*/)
    {
        word = _word;
        typeIndex = 0;

        //display = _display;
        //display.SetWord(this);
    }



    public char GetNextLetter()
    {


        return word[typeIndex];
        
    }
    
    public void TypeLetter()
    {
        typeIndex++;
        RemoveLetterBool = true;
        //display.RemoveLetter();
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped) {
            DeleteLetterBool = true;
            //display.RemoveWord();
        }
        return wordTyped;
    }

}
