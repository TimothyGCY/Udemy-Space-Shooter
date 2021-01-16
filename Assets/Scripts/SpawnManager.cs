using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] powerupType;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        while (_player.GetPlayerLife() > 0)
        {
            Vector3 spawnPos;
            do
            {
                spawnPos = new Vector3(Random.Range(-9f, 9f), 7, 0);
            } while (Physics.OverlapSphere(spawnPos, 1f).Length > 1);

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        GameObject powerup = null;

        while (_player.GetPlayerLife() > 0)
        {
            yield return new WaitForSeconds(Random.Range(15f, 30f));
            int randPU = Random.Range(1, 4);
            powerup = powerupType[randPU - 1];
            Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 7, 0);
            if (powerup != null)
            {
                Instantiate(powerup, spawnPos, Quaternion.identity);
            }
        }
    }
}
