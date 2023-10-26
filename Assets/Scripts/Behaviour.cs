using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour
{
    private float m_weight;

    public Behaviour(float weight)
    {
        m_weight = weight;
    }

    public virtual Vector2 BehaviorUpdate(Agent agent) { return Vector2.zero; }

    public float GetWeight() { return m_weight; }
}
