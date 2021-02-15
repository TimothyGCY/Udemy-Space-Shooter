using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;
    private AudioSource _audioSource;

    private int _enemySpd;
    private GameManager _gameManager;
    private Player _player;
    private UIManager _uiManager;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        transform.position = new Vector3(Random.Range(-9f, 9f), 7f, 0);
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
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
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Enemy>().enabled = false;
            _anim.SetTrigger("OnEnemyDestroy");
            Destroy(gameObject, 2.8f);
            _audioSource.Play();
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore();
                _uiManager.UpdateScore(_player.GetPlayerScore());
            }
        }

        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDestroy");
            Destroy(gameObject, 2.8f);
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
        // _anim.ResetTrigger("OnEnemyDestroy");
    }
}
