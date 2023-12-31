using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Behaviour
{
    private List<Agent> m_obsticles;

    public Flee(float wegith) : base(wegith) { }

    public override Vector2 BehaviorUpdate(Agent agent)
    {
        if (agent == null)
            return Vector2.zero;

        // Find the clsoest
        float smallestDistance = float.MaxValue;
        int closetestObsticle = 0;
        for (int i = 0; i < m_obsticles.Count; i++)
        {
            float currentDistance = Vector2.Distance(agent.GetPos(), m_obsticles[i].GetPos());
            if (currentDistance < smallestDistance)
            {
                smallestDistance = currentDistance;
                closetestObsticle = i;
            }
        }

        //                                                Dir                            Half power         Force
        return ((agent.GetPos() - m_obsticles[closetestObsticle].GetPos()).normalized * GetWeight()) - agent.GetVel();
    }

    public void SetObsticle(List<Agent> agents)
    {
        if (agents.Count != 0)
        {
            m_obsticles = agents;
        }
        else
        {
            Debug.Log("Failed to set obsticle for flee behaviour, check passed object");
        }
    }
}
