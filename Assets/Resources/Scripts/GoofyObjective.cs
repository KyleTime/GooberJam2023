using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofyObjective : MonoBehaviour
{
    public float distance = 1.2f;
    public bool active = false;
    public bool complete = false;

    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.END)
            return;

        if (active && !complete && Vector2.Distance(GoofyControl.inst.transform.position, transform.position) < distance)
        {
            complete = true;
            GoofyControl.inst.Paralyze(2f);
            StartCoroutine(ColorChange());
        }
    }

    public IEnumerator ColorChange()
    {
        float t = 0;
        while(t < 1)
        {
            spr.color = Color.Lerp(Color.white, Color.yellow, t);
            t += Time.deltaTime / 2;

            yield return null;
        }
    }
}
