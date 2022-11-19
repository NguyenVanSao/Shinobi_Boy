using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int _score;
    [SerializeField] int _scoreToReach;
    [SerializeField] Text _scoreText;
    [SerializeField] GameObject _setting;
    [SerializeField] int currentScene;

    // Start is called before the first frame update
    public void ScoreIncrease()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    public void Setting()
    {
        _setting.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _setting.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CheckScore()
    {
        if (_score == _scoreToReach)
        {
            SceneManager.LoadScene(currentScene+1);
        }
    }
}
