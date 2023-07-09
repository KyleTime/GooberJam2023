using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScare : MonoBehaviour
{
    public float switchCooldown;

    public float maxDistance;

    float distFromPlayer;

    public bool switchEnabled = true;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.END)
            return;

        if (switchEnabled)
        {
            distFromPlayer = Vector3.Distance(PlayerControl.playerPos.position, transform.position);
            if (distFromPlayer < maxDistance)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    switchEnabled = false;

                    Invoke(nameof(EnableSwitch), switchCooldown);
                    
                    GoofyMeter.FearParalyze(0.05f);
                }
            }
        }
    }

    public void EnableSwitch()
    {
        switchEnabled = true;
    }
}
