using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    public float force;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        

        Vector3 direction = player.transform.position - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
