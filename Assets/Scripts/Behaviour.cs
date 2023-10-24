using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour
{
    public virtual Vector2 BehaviorUpdate(Agent agent) { return Vector2.zero; }
}
