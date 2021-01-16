using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupItem : MonoBehaviour
{
    [SerializeField]
    private float _itemDropSpeed = 3.0f;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.IsGameOver())
        {
            transform.Translate(Vector3.down * _itemDropSpeed * Time.deltaTime);
            if (transform.position.y < -5.0f)
            {
                Destroy(gameObject);
            }
        }
        else Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                player.GetPowerUp(gameObject.tag);
                Destroy(gameObject);
            }
        }
    }
}
