using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofyMeter : MonoBehaviour
{
    public static GoofyMeter inst;
    
    [Header("Settings")]
    static float tension = 0;
    public float tenseSeconds = 1; //how many seconds it takes to fully become tense
    public float decaySeconds = 1; //how many seconds it takes to fully lose tension
    bool decay = false;

    [Header("Display")]
    public Transform Fill; //the inner part of the tension bar

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!decay)
        {
            tension += Time.deltaTime / tenseSeconds;

            if (tension > 1)
            {
                tension = 1;
                decay = true;
            }
        }
        else
        {
            tension -= Time.deltaTime / decaySeconds;
            
            if(tension < 0)
            {
                tension = 0;
            }
        }
    }

    void DisplayTension()
    {

    }

    void Scare(float power)
    {
        tension -= power;
        decay = false;
    }
}
