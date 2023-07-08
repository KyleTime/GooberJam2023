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

    public float jumpTime = 0;
    public float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GetComponent<Transform>();
        rgd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.END)
            return;

        if(jumpTime > 0)
        {
            jumpTime -= Time.deltaTime;

            if(Vector2.Distance(transform.position, GoofyControl.inst.transform.position) < 1f)
            {
                Debug.Log("JUMP SCARE!");
                Level.JUMPSCARE();
            }

            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rgd.velocity = new Vector2(horizontal * speed, vertical * speed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jumpscare();
        }
    }

    void Jumpscare()
    {
        jumpTime = 1;
        rgd.velocity = (GoofyControl.inst.transform.position - transform.position).normalized * speed * 10;
    }
}
