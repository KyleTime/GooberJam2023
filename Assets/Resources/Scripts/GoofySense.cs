using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofySense : MonoBehaviour
{
    public static GoofySense inst;

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Sound(mouse, 5, 0.1f);
        }
    }

    /// <summary>
    /// Whenever a sound happens, call this function
    /// </summary>
    /// <param name="location">location of the sound</param>
    /// <param name="range">how far away the sound can be heard</param>
    public static void Sound(Vector2 location, float range, float power)
    {
        if (Vector2.Distance(inst.transform.position, location) > range)
            return;

        GoofyControl.inst.Fear(location);
        GoofyControl.inst.fearRange = range;
        
        if(power > 0)
        {
            GoofyMeter.Scare(power);
        }
        else
        {
            GoofyMeter.Tense(-power);
        }
    }
}
