using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreTxt;
    [SerializeField]
    private Sprite[] _lifeSprite;
    [SerializeField]
    private Image _lifeImg;
    [SerializeField]
    private Text _gameOverTxt;
    [SerializeField]
    private Text _restartTxt;

    private GameManager _gameManager;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreTxt.text = "Score\n" + _player.GetPlayerScore().ToString();
        _lifeImg.sprite = _lifeSprite[_player.GetPlayerLife()];
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOverTxt.gameObject.SetActive(false);
        _restartTxt.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreTxt.text = "Score\n" + score.ToString();
    }

    public void UpdateLife(int life)
    {
        _lifeImg.sprite = _lifeSprite[life];
    }

    public void ShowGameOver()
    {
        _gameManager.GameOver();
        _gameOverTxt.gameObject.SetActive(true);
        _restartTxt.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        StartCoroutine(RestartFlickerRoutine());
        StartCoroutine(DestroyAllEnemy());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverTxt.color = new Color(_gameOverTxt.color.g, _gameOverTxt.color.b, 0);
            while (_gameOverTxt.color.a < 1.0f)
            {
                _gameOverTxt.color = new Color(_gameOverTxt.color.r, _gameOverTxt.color.g, _gameOverTxt.color.b, _gameOverTxt.color.a + Time.deltaTime);
                yield return null;
            }

            _gameOverTxt.color = new Color(_gameOverTxt.color.g, _gameOverTxt.color.b, 1);
            while (_gameOverTxt.color.a > 0f)
            {
                _gameOverTxt.color = new Color(_gameOverTxt.color.r, _gameOverTxt.color.g, _gameOverTxt.color.b, _gameOverTxt.color.a - Time.deltaTime);
                yield return null;
            }
        }
    }

    IEnumerator RestartFlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.505f);
            _restartTxt.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.505f);
            _restartTxt.gameObject.SetActive(true);
        }
    }

    IEnumerator DestroyAllEnemy()
    {
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(o);
            yield return null;
        }
    }
}
