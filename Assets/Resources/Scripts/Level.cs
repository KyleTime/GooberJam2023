using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level inst;

    public static bool END = false;
    bool init = false;

    public int score = 0;
    public int requiredScore = 1000;

    public int currentNode = -1;
    public GoofyObjective[] nodes; //nodes that the streamer will traverse
    bool choose = false;

    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!init)
        {
            //animation here
        }

        if (Level.END)
            return;

        if (!choose)
        {
            currentNode++;

            if (currentNode > nodes.Length - 1)
            {
                ESCAPED();
                return;
            }

            choose = true;

            //Debug.Log("NODE LENGTH: " + nodes.Length);
            GoofyControl.inst.goal.position = nodes[currentNode].transform.position;
            nodes[currentNode].active = true;
        }
        else if(Vector2.Distance(GoofyControl.inst.transform.position, nodes[currentNode].transform.position) < 1)
        {
            choose = false;
        }
    }

    public static void Score(float power)
    {
        int addScore = (int)((power + 1f) * (power + 1f) * 100) - 100;
        addScore *= 2;
        if(addScore > 0)
            addScore += 10 - addScore % 10;
        Debug.Log("SCORED " + addScore + " POINTS!");
        inst.score += addScore;
    }

    public static void JUMPSCARE()
    {
        Score(GoofyMeter.tension); //dump tension into score

        Debug.Log("Final Score was: " + inst.score);

        END = true;

        //TODO: jumpscare animation
    }

    public static void ESCAPED()
    {
        Debug.Log("Final Score was: " + inst.score);
        END = true;
    }
}
