using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Agent
{
    private List<Behaviour> m_behaviourList = new List<Behaviour>();
    private Vector2 m_pos;
    private Vector2 m_vel;
    private string m_name;
    private SpriteRenderer m_sprite = new SpriteRenderer();


    public Agent(string name, SpriteRenderer renderer)
    {
        m_name = name;
        m_vel = Vector2.zero;
        m_sprite = renderer;
    }

    // Add a behaviour to the agent
    public void AddBehaviour(Behaviour behaviour)
    {
        if (behaviour != null)
        {
            m_behaviourList.Add(behaviour);
        }
    }

    public void AgentUpdate()
    {
        // Force is equal to zero
        Vector2 force = Vector2.zero;

        // for each Behaviour in Behaviour list
        foreach (Behaviour currentBehavior in m_behaviourList)
        {
            // Call the Behaviourís Update function and add the returned value to Force
            force += currentBehavior.BehaviorUpdate(this);
        }

        // Add Force multiplied by delta time to Velocity
        m_vel += force * Time.deltaTime;

        // Add Velocity multiplied by delta time to Position
        m_pos += m_vel * Time.deltaTime;
        //Debug.Log(m_name + "'s current position is: " + m_pos);

        // Update the position of the sprite
        m_sprite.transform.position = m_pos;
    }

    private void Update()
    {
        // Since the transform is in 3d space, update the Vector3 postion every frame. We will pretend is it 2d
        //transform.position = new Vector3(m_pos.x, m_pos.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        // Draw the current position of the agent using a cube for now
        Gizmos.DrawCube(m_pos, new Vector3(1,1,1));
    }

    // Setters
    public void SetPos(Vector2 position) { m_pos = position; }
    public void SetVel(Vector2 velocity) { m_vel = velocity; }

    // Getters
    public Vector2 GetPos() { return m_pos; }
    public Vector2 GetVel() { return m_vel; }
}
