using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    bool progress = false;

    public float interactDistance = 2;

    float openTime = 1;
    
    Collider2D col;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //so that the player can open and close the door
        if (!progress && Input.GetKeyDown(KeyCode.Space) && PlayerControl.inst.jumpTime <= 0 && Vector2.Distance(PlayerControl.playerPos.position, transform.position) < interactDistance)
        {
            PlayerControl.inst.jumpTime = openTime;
            PlayerControl.inst.rgd.velocity = new Vector2();
            Invoke("OpenDoor", openTime);
            progress = true;
        }

        //so that streamer goofy can open and close the door
        if (!progress && !open && Vector2.Distance(GoofyControl.inst.transform.position, transform.position) < interactDistance)
        {
            GoofyControl.inst.Paralyze(openTime);
            Invoke("OpenDoor", openTime);
            progress = true;
        }

        //control collider
        col.enabled = !open;

        //temporary render code
        if (open)
            spr.color = Color.blue;
        else
            spr.color = Color.red;
    }

    void OpenDoor()
    {
        open = !open;
        progress = false;

        if(!open)
        {
            GoofySense.Sound(transform.position, 15f, 0.3f);
        }
    }
}
