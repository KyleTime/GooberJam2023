using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScare : MonoBehaviour
{
    PolygonCollider2D roomCollider;

    public float switchCooldown;
    public float flickerTimer;

    public float maxDistance;

    float distFromPlayer;

    public bool switchEnabled = true;

    Vector3 lightPosition;

    private void Awake()
    {
        roomCollider = GetComponent<PolygonCollider2D>();
        roomCollider.enabled = false;

        lightPosition = GetComponent<Transform>().position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchEnabled)
        {
            distFromPlayer = Vector3.Distance(PlayerControl.playerPos.position, lightPosition);
            if (distFromPlayer < maxDistance)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    roomCollider.enabled = true;
                    switchEnabled = false;

                    Invoke(nameof(StopFlicker), flickerTimer);
                    Invoke(nameof(EnableSwitch), switchCooldown);
                }
            }
        }
    }

    public void StopFlicker()
    {
        roomCollider.enabled = false;
    }

    public void EnableSwitch()
    {
        StopFlicker();
        switchEnabled = true;
    }
}
