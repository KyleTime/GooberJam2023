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
    bool decay = true;

    [Header("Display")]
    public Transform BarAnchor; //controls the inner part of the tension bar

    Vector2 goalPos;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTension();

        DisplayTension();
    }

    void UpdateTension()
    {
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
        Scare(power);

        GoofyControl.inst.Paralyze(power * 5);
    }

    public static void Scare(float power)
    {
        tension -= power;
        inst.decay = false;
    }
}
