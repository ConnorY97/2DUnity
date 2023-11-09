using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Alignment : Behaviour
{
    public Alignment(float weight, float distance, float buffer) : base(weight) { m_neighbourDistance = distance; m_directionbuffer = buffer; }

    private float m_neighbourDistance = 10.0f;
    private float m_directionbuffer = 10.0f;
    private List<Agent> m_neighbourhood = new List<Agent>();

    public override Vector2 BehaviorUpdate(Agent agent)
    {
        Vector2 alignmentForce = Vector2.zero;
        int closeNeighbourCount = 0;
        foreach (Agent neighbour in m_neighbourhood)
        {
            // Skip self
            if (neighbour == agent)
                continue;

            if (Vector2.Distance(agent.GetPos(), neighbour.GetPos()) < m_neighbourDistance)
            {
                float dot = Vector2.Dot(agent.GetVel(), neighbour.GetVel());
                if (dot > m_directionbuffer)
                {
                    alignmentForce = (agent.GetVel() + neighbour.GetVel()).normalized;
                    closeNeighbourCount++;
                }
            }
        }

        // For the first call boids may not have any veolcity
        // so we need to check that there is actually force being applied
        if (alignmentForce != Vector2.zero)
        {
            // Average alignment force
            alignmentForce /= closeNeighbourCount;
            Vector2 force = (alignmentForce + agent.GetVel()) * GetWeight();

            return force;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public void SetNeighbourHood(List<Agent> neighbourhood) {  m_neighbourhood = neighbourhood; }
    public void UpdateDistance(float distance) { m_neighbourDistance = distance; }
    public void UpdateBuffer(float buffer) { m_directionbuffer = buffer; }
}
