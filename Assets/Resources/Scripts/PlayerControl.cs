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

    public Vector2Int dir = new Vector2Int();
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        playerPos = transform;
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Animate(string idle, string run)
    {
        if (anim.GetFloat("speed") == 0)
        {
            anim.Play(idle);
        }
        else
        {
            anim.Play(run);
        }
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

        if (horizontal != 0 || vertical != 0)
        {
            dir = new Vector2Int((int)horizontal, (int)vertical);
            anim.SetFloat("speed", 1);
        }
        else
            anim.SetFloat("speed", 0);

        switch(dir.x)
        {
            case 0:
                switch(dir.y)
                {
                    case 1:
                        Animate("IdleUp", "RunUp");
                        break;
                    case -1:
                        Animate("IdleDown", "RunDown");
                        break;
                }    
                break;
            case 1:
                switch (dir.y)
                {
                    case 0:
                        Animate("IdleRight", "RunRight");
                        break;
                    case -1:
                        Animate("IdleDownRight", "RunDownRight");
                        break;
                    case 1:
                        Animate("IdleUpRight", "RunUpRight");
                        break;
                }
                break;
            case -1:
                switch (dir.y)
                {
                    case 0:
                        Animate("IdleLeft", "RunLeft");
                        break;
                    case -1:
                        Animate("IdleDownLeft", "RunDownLeft");
                        break;
                    case 1:
                        Animate("IdleUpLeft", "RunUpLeft");
                        break;
                }
                break;
        }

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
            if(Vector2.Distance(steppyStart, transform.position) > 1)
            {
                GoofySense.Sound(transform.position, 12, -0.04f);
                steppyStart = transform.position;
            }
        }

        rgd.velocity = new Vector2(horizontal * speed * speedMod, vertical * speed * speedMod);
    }

    void Jumpscare()
    {
        jumpTime = 0.2f;
        rgd.velocity = (GoofyControl.inst.transform.position - transform.position).normalized * speed * 10;
    }
}
