using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Behaviour
{
    public Player(float weight) : base(weight) { }

    public override Vector2 BehaviorUpdate(Agent agent)
    {
        if (agent == null)
            return Vector2.zero;
        
        Vector2 dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dir = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dir = -Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = -Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetVel(Vector2.zero);
        }

        return dir * GetWeight() * 100;
    }
}
