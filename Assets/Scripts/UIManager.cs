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

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreTxt.text = "Score: " + _player.GetPlayerScore().ToString() + "    ";
        _lifeImg.sprite = _lifeSprite[_player.GetPlayerLife()];
        _gameOverTxt.enabled = false;
    }

    public void UpdateScore(int score)
    {
        _scoreTxt.text = "Score: " + score.ToString() + "    ";
    }

    public void UpdateLife(int life)
    {
        _lifeImg.sprite = _lifeSprite[life];
    }

    public void ShowGameOver()
    {
        _gameOverTxt.enabled = true;
    }
}
