using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static int scoreValue;
    public static int highScore;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        var scene = SceneManager.GetActiveScene();
        if (scene == SceneManager.GetSceneByName("Start"))
        {
            score.text = "Game Over!" + "\n" + "Your Score: " + scoreValue + "\n" + "Highscore: " + highScore + "\n\n\n\n\n\n" + "Click to Reset";
        }
        if (scene == SceneManager.GetSceneByName("Main"))
        {
            score.text = "Score: " + scoreValue;
        }
    }
}
