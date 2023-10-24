using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : Behaviour
{
    private Agent m_target;
    public float m_speed = 100.0f;


    public override Vector2 BehaviorUpdate(Agent agent)
    {
        // Checking if the provided agent is valid
        if (agent == null)
            return Vector2.zero;

        // Positions
        Vector2 currentPos = agent.GetPos();
        Vector2 targetPos = m_target.GetPos();

        // Calculate the vector describing the direction to the target and normalize it
        // The direction is calulated by targets position subtracted from our own
        Vector2 dir = (targetPos - currentPos).normalized;

        // Multiply the direction by the speed we want the agent to move
        dir *= m_speed;

        // Subtract the agent’s current velocity from the result to get the force we need to apply
        Vector2 force = dir - m_target.GetVel();

        // return the force
        return force;
    }

    public void SetTarget(Agent agent) { m_target = agent; }
}
