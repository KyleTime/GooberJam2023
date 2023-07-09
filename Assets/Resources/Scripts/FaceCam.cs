using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCam : MonoBehaviour
{
    public Sprite[] faces;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = (int)(GoofyMeter.tension * 5) + 1;
        if (i == 6)
            i = 5;
        spr.sprite = faces[i];
    }
}
