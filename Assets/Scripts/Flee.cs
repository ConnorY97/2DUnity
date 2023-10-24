using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Behaviour
{
    private Agent m_obsticle;

    public override Vector2 BehaviorUpdate(Agent agent)
    {
        if (agent == null)
            return Vector2.zero;

        return ((agent.GetPos() - m_obsticle.GetPos()).normalized * 75.0f) - agent.GetVel();
    }

    public void SetObsticle(Agent agent)
    {
        if (agent != null)
        {
            m_obsticle = agent;
        }
        else
        {
            Debug.Log("Failed to set obsticle for flee behaviour, check passed object");
        }
    }
}
