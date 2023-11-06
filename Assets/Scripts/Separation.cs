using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Separation : Behaviour
{
    public Separation(float weight) : base(weight) { }

    private List<Agent> m_neighbourhood;
    public override Vector2 BehaviorUpdate(Agent agent)
    {
        Vector2 separationForce = Vector2.zero;

        foreach (Agent neighbour in m_neighbourhood)
        {
            // Skip itself
            if (neighbour == agent)
                continue;

            separationForce += (agent.GetPos() - neighbour.GetPos());//.normalized;
        }

        // Average separation force
        separationForce /= m_neighbourhood.Count;
        Vector2 force = (separationForce - agent.GetVel()) * GetWeight();
        //Debug.Log(agent.GetName() + "'s separation force is " + force);
        return force;
    }

    public void SetNeighbourhood(List<Agent> neighbourhood) { m_neighbourhood = neighbourhood; }
}
