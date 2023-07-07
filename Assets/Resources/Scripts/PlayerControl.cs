using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static Transform playerPos;
    Rigidbody2D rgd;

    float vertical;
    float horizontal;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GetComponent<Transform>();
        rgd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rgd.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
