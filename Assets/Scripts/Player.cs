using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int PLAYER_SHOOT_LASER = 0, PLAYER_GET_POWER_UP = 1, PLAYER_GET_DAMAGE = 2;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _leftBurn, _rightBurn;

    [SerializeField]
    private AudioClip[] _audioClips;
    private AudioSource _audioSource;

    private UIManager _uiManager;

    private int _playerSpd = 15, _playerLife = 3, _score = 0;
    private float _fireRate = 0.25f, _canFire = -1f;
    private bool _tripleShotEnabled = false, _shieldEnabled = false, _speedUpEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3.8f, 0);
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageControl();
    }

    void ManageControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _playerSpd * Time.deltaTime);

        float posX = transform.position.x, posY = transform.position.y;
        transform.position = new Vector3(Mathf.Clamp(posX, -9, 9), Mathf.Clamp(posY, -3.8f, 0), 0);

        if (Input.GetMouseButtonDown(0) && Time.time > _canFire)
        {
            ShootLaser();
        }

    }

    void ShootLaser()
    {
        _canFire = Time.time + _fireRate;
        GameObject laser = _tripleShotEnabled ? _tripleShotPrefab : _laserPrefab;
        Instantiate(laser, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

        PlaySoundFX(PLAYER_SHOOT_LASER);
    }

    public void Damage()
    {
        if (_shieldEnabled == false)
        {
            _playerLife--;
            _uiManager.UpdateLife(GetPlayerLife());
            if (_playerLife > 0)
            {
                _tripleShotEnabled = false;
                if (_speedUpEnabled == true)
                {
                    _speedUpEnabled = false;
                    _fireRate *= 2;
                    _playerSpd /= 2;
                }
                transform.position = new Vector3(0, -3.8f, 0);
            }
            else
            {
                Destroy(gameObject);
                _uiManager.ShowGameOver();
            }
            PlaySoundFX(PLAYER_GET_DAMAGE);
        }
        else _shieldEnabled = false;

        if (_playerLife == 2)
        {
            _leftBurn.SetActive(true);
        }
        if (_playerLife == 1)
        {
            _rightBurn.SetActive(true);
        }
    }

    public void GetPowerUp(string powerup)
    {
        switch (powerup)
        {
            case "TripleShotPU":
                _tripleShotEnabled = true;
                break;
            case "ShieldPU":
                _shieldEnabled = true;
                Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
                break;
            case "SpeedUp":
                _speedUpEnabled = true;
                _fireRate /= 2;
                _playerSpd *= 2;
                break;
            default: break;
        }
        PlaySoundFX(PLAYER_GET_POWER_UP);
        StartCoroutine(PowerDownRoutine(powerup));
    }

    IEnumerator PowerDownRoutine(string powerup)
    {
        yield return new WaitForSeconds(10.0f);
        switch (powerup)
        {
            case "TripleShotPU":
                _tripleShotEnabled = false;
                break;
            case "SpeedUp":
                _speedUpEnabled = false;
                _fireRate *= 2;
                _playerSpd /= 2;
                break;
            default: break;
        }
    }

    public int GetPlayerLife()
    {
        return _playerLife;
    }

    public void UpdateScore()
    {
        _score += 100;
    }

    public int GetPlayerScore()
    {
        return _score;
    }

    private void PlaySoundFX(int type)
    {
        _audioSource.clip = _audioClips[type];
        _audioSource.Play();
    }
}
