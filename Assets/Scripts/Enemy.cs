using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _enemySpd = 5;

    private Player _player;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        transform.position = new Vector3(Random.Range(-9f, 9f), 7f, 0);
        StartCoroutine(IncreaseDifficultyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore();
                _uiManager.UpdateScore(_player.GetPlayerScore());
            }
        }

        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator IncreaseDifficultyRoutine()
    {
        // current issue, only change a single instance
        yield return new WaitForSeconds(30.0f);
        _enemySpd += 5;
    }
}
