using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private float _animDuration = 2.8f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _animDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
