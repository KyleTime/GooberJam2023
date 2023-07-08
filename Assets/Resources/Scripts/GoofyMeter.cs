using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofyMeter : MonoBehaviour
{
    public static GoofyMeter inst;
    
    [Header("Settings")]
    public static float tension = 0;
    public float tenseSeconds = 1; //how many seconds it takes to fully become tense
    public float decaySeconds = 1; //how many seconds it takes to fully lose tension
    bool decay = true;

    [Header("Display")]
    public Transform BarAnchor; //controls the inner part of the tension bar

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.END)
            return;

        UpdateTension();

        DisplayTension();
    }

    void UpdateTension()
    {
        if (GoofyControl.inst.stunTimer > 0)
            return;

        if (!decay)
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

            if (tension < 0)
            {
                tension = 0;
            }
        }
    }

    void DisplayTension()
    {
        BarAnchor.localScale = new Vector3(tension, 1);
    }

    public static void FearParalyze(float power)
    {
        Tense(power);

        GoofyControl.inst.Paralyze(Mathf.Abs(power) * 20);
    }

    //used to increment Tension
    public static void Tense(float power)
    {
        tension += power;

        tension = Mathf.Clamp(tension, 0, 1);

        inst.decay = false;
    }

    //used to release tension and score points
    public static void Scare(float power)
    {
        tension -= power;

        float score = power;

        if(tension < 0)
        {
            score = tension + power;
        }

        Level.Score(score); //send score to Level for calculations

        tension = Mathf.Clamp(tension, 0, 1);        
    }
}
