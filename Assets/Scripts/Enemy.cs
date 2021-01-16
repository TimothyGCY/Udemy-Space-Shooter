using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _enemySpd;

    private GameManager _gameManager;
    private Player _player;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        transform.position = new Vector3(Random.Range(-9f, 9f), 7f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _enemySpd = _gameManager.GetCurrentSpeed();
        transform.Translate(Vector3.down * _enemySpd * Time.deltaTime);
        float randomX = Random.Range(-9f, 9f);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(randomX, 7.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore();
                _uiManager.UpdateScore(_player.GetPlayerScore());
            }
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
    }
}
