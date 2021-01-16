using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupItem : MonoBehaviour
{
    [SerializeField]
    private float _itemDropSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _itemDropSpeed * Time.deltaTime);
        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this.gameObject.tag);
        Debug.Log("collided with" + collision.tag.ToString());
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collide with player");
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                player.GetPowerUp(this.gameObject.tag);
                Destroy(this.gameObject);
            }
        }
    }
}
