using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float lifetime = 5f;
    private float timePassed = 0f;

    // Start is called before the first frame update

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
}
