using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl inst;

    public static Transform playerPos;
    public Rigidbody2D rgd;

    float vertical;
    float horizontal;

    public float speed;

    public float jumpTime = 0;
    public float cooldown = 0;

    public Vector2 steppyStart;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        playerPos = transform;
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

            if(rgd.velocity.magnitude > 10 && Vector2.Distance(transform.position, GoofyControl.inst.transform.position) < 1f)
            {
                Debug.Log("JUMP SCARE!");
                Level.JUMPSCARE();
            }

            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        float speedMod = 1;
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Jumpscare();
            return;
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            steppyStart = transform.position;
        }
        else if(Input.GetKey(KeyCode.X))
        {
            speedMod = 0.33f;
            //you took a step
            if(Vector2.Distance(steppyStart, transform.position) > 2)
            {
                GoofySense.Sound(transform.position, 12, -0.01f);
                steppyStart = transform.position;
            }
        }

        rgd.velocity = new Vector2(horizontal * speed * speedMod, vertical * speed * speedMod);
    }

    void Jumpscare()
    {
        jumpTime = 1;
        rgd.velocity = (GoofyControl.inst.transform.position - transform.position).normalized * speed * 10;
    }
}
