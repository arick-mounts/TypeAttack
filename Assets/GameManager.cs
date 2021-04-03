using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static float fallSpeed = 1;
    public static int score = 0;
    public static string player = "PlayerName";
    public static bool isMusic;

    public List<WordDisplay> words;

    public WordSpawner wordSpawner;

    public bool hasActiveWord;
    public bool isPaused;

    public GameObject Panel;

    public WordDisplay activeWord;

    public Text scoreText;
    public Text NameText;




    private void Start()
    {
        GameManager.score = 0;
        if (NameText)
        {
            NameText.text = GameManager.player;
        }
        isPaused = false;

        switch (PlayerPrefs.GetInt("MusicSetting", 0))
        {
            case (0): isMusic = false;  break;
            case (1): isMusic = true; break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }

    public void ToggleMusic(bool music)
    {
        if (isMusic)
        {
            isMusic = false;
            PlayerPrefs.SetInt("MusicSetting", 0);
        }
        else
        {
            isMusic = true;
            PlayerPrefs.SetInt("MusicSetting", 1);
        }
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            Panel.SetActive(true);
            Panel.GetComponent<RectTransform>().SetAsLastSibling();
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Panel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void UpdateScore()
    {

        GameManager.score++;
        scoreText.text = "score: " + GameManager.score.ToString();
    }
    public void UpdatePlayer(string name)
    {
        GameManager.player = name;
    }


    public void AddWord()
    {

        Word word = new Word(WordGenerator.getRandomWord());
        words.Add(
        wordSpawner.SpawnWord(word));
    }

    public void TypeLetter(char letter)
    {
        if (!isPaused)
        {
            if (hasActiveWord)
            {
                if (activeWord.word.GetNextLetter() == letter)
                {
                    activeWord.word.TypeLetter();
                }
            }
            else
            {
                foreach (WordDisplay word in words)
                {
                    if (word.word.GetNextLetter() == letter)
                    {
                        activeWord = word;
                        hasActiveWord = true;
                        word.word.TypeLetter();
                        break;
                    }
                }
            }
        }

        if (hasActiveWord && activeWord.word.WordTyped()) 
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }

  

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void updateFallSpeed(float speed)
    {
        GameManager.fallSpeed = speed;
    }
  public void clearWords()
    {

        foreach(WordDisplay word in words)
        {
            Destroy (word.gameObject);
        }
        words.Clear();
    }

    public void SaveGame()
    {
        //create save object
        Save save = CreateSaveGameObject();

        //save object to file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        //reset game to default state
        ResetGame();

        Debug.Log("Game saved");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        foreach (WordDisplay word in words)
        {
            save.currentWords.Add(word.word);
        }

        save.score = score;
        save.player = player;
        return save;
    }

    public void ResetGame()
    {
        score = 0;
        player = "PlayerName";
        scoreText.text = score.ToString();
        NameText.text = player;
        clearWords();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //Clear game
            ResetGame();

            //open save file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();


            foreach (Word word in save.currentWords)
            {
                words.Add(wordSpawner.SpawnWord(word, word.height, word.horiz));
                
            }

            score = save.score;
            player = save.player;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");

        }
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log ("Saving as json " + json);
    }
}
