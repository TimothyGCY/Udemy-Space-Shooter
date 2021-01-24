using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;

    private float _rotateSpd = 3.0f;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpd * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
            _spawnManager.StartGameRountine();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) player.Damage();
        }
    }
}
