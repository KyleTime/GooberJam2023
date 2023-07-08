using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoofyControl : MonoBehaviour
{
    public static GoofyControl inst;

    public enum State { Following, Wandering, Fearing }

    public float fearRange = 5;
    const float fearTime = 5;
    float fearTimer = 0;

    //stun stuff
    public float stunTimer = 0;
    public State state;


    public float direction = 0; //direction we're currently moving in (radians)
    public float magnitude = 0; //current speed of the character 
    public float maxSpeed = 1; //max speed of the character

    public Transform goal;
    public Transform fear;

    RaycastHit2D[] rays = new RaycastHit2D[5];

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
            return;
        }
        else if(fearTimer > 0)
        {
            fearTimer -= Time.deltaTime;
            if(fearTimer <= 0)
            {
                fearTimer = 0;
                state = State.Following;
            }
        }

        CheckAround();
        Move();
        ChangeSpeed();
    }

    void CheckAround()
    {
        float nextTheta = direction;
        float rayLength = 2;

        int numChecks = 0; //check how many times we see a wall
        for(int i = 0; i < 5; i++)
        {
            float rayTheta = direction + (i - 2) * Mathf.PI / 4;

            rays[i] = Physics2D.Raycast(transform.position, GetDirection(rayTheta), rayLength);

            float rotatae = 0;
            if(rays[i].collider != null)
            {
                rotatae = -(rayTheta - nextTheta) * Mathf.Abs(i - 2) * (rayLength / (rays[i].distance + 0.01f)) * maxSpeed * Time.deltaTime;
                numChecks++;
            }
            else
            {
                rotatae = -(rayTheta - nextTheta) * Mathf.Abs(i - 2) * maxSpeed * Time.deltaTime;
            }

            nextTheta = RotateTheta(nextTheta, rotatae);
        }

        direction = nextTheta;

        if (state == State.Following && numChecks < 4)
            TowardsGoal(numChecks);
        
        if (state == State.Fearing)
            AwayFromFear();
        
    }

    void TowardsGoal(int numHits)
    {
        float thetaToGoal = Mathf.Atan2(goal.position.y - transform.position.y, goal.position.x - transform.position.x);
        direction = RotateTheta(direction, Mathf.Clamp((thetaToGoal - direction) * (4f / (numHits + 1f)) * Time.deltaTime, -Mathf.PI / 6, Mathf.PI / 6));
    }

    void AwayFromFear()
    {
        float thetaToGoal = Mathf.Atan2(goal.position.y - transform.position.y, goal.position.x - transform.position.x) + Mathf.PI;
        direction = RotateTheta(direction, (thetaToGoal - direction) * 20 * Time.deltaTime);
    }

    void Move()
    {
        transform.position += GetDirection(direction) * magnitude * Time.deltaTime;

        if (state == State.Fearing && Vector2.Distance(transform.position, fear.position) > fearRange)
            state = State.Following;

    }

    void ChangeSpeed()
    {
        if (state == State.Following)
        {
            float dist = Vector2.Distance(transform.position, goal.position);
            if (dist < 2)
            {
                magnitude = maxSpeed * dist / 2;
            }
        }
        else if(state == State.Fearing)
        {
            magnitude = 2;
        }
    }

    Vector3 GetDirection(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    float RotateTheta(float theta, float factor)
    {
        float end = theta + factor;
        if(end < -Mathf.PI*2)
        {
            end = end + Mathf.PI * 2;
        }
        else if(end > Mathf.PI*2)
        {
            end = end - Mathf.PI * 2;
        }

        return end;
    }

    public void Paralyze(float time)
    {
        stunTimer = time;
    }

    public void Fear(float power, Vector2 position)
    {
        fear.position = position;
        state = State.Fearing;
        fearTimer = fearTime;
        GoofyMeter.Scare(power);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for(int i = 0; i < 5; i++)
        {
            if(rays[i].collider != null)
                Gizmos.DrawLine(transform.position, rays[i].point);
        }

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + GetDirection(direction) * 5);
    }
}
