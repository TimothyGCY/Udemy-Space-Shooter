using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    private int _initSpeed = 5;

    private void Start()
    {
        StartCoroutine(IncreaseDifficultyRoutine());
    }

    private void Update()
    {
        //if (Input.anyKey && _isGameOver) // this include mouse click
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(0); // current game scene
            // SceneManager.LoadScene("Game"); // when you have multi scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }

    public int GetCurrentSpeed()
    {
        return _initSpeed;
    }

    IEnumerator IncreaseDifficultyRoutine()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        while (player.GetPlayerLife() > 0)
        {
            yield return new WaitForSeconds(30f);
            _initSpeed += 5;
        }
    }
}
