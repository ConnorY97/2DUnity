using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    [Header("Behaviour weights")]
    public float playerWeight = 100.0f;
    public float seekWeight = 100.0f;
    public float separationWeight = 100.0f;
    public float fleeWeight = 100.0f;

    [Header("Entity counts")]
    public int m_enemyCount = 1;
    public int m_obsticleCount = 1;

    [Header("Rendering")]
    public SpriteRenderer m_playerSprite;
    public SpriteRenderer m_seekSprite;
    public SpriteRenderer m_obsticleSprite;


    private Agent m_player;
    private List<Agent> m_seekers = new List<Agent>();
    private List<Agent> m_obsticles = new List<Agent>();

    private Flee m_fleeBehaviour = null;
    private Seek m_seekBehaviour = null;
    private Separation m_separationBehaviour = null;

    private void Start()
    {
        // Agent init
        m_player = new Agent("Player", m_playerSprite);
        m_player.SetPos(new Vector2(10.0f, 10.0f));
        m_player.AddBehaviour(new Player(playerWeight));

        // Set initial position of agents
        m_player.AgentUpdate();

        // Obsticle init
        for (int i = 0; i < m_obsticleCount; i++)
        {
            var tempSprite = Instantiate(m_obsticleSprite, this.transform);
            Agent obsticle = new Agent("Obsticle" + 1, tempSprite);
            float xPos = Random.Range(-100.0f, 100.0f);
            float yPos = Random.Range(-100.0f, 100.0f);
            obsticle.SetPos(new Vector2(xPos, yPos));
            obsticle.AgentUpdate();
            m_obsticles.Add(obsticle);
        }

        // Behaviours init
        m_fleeBehaviour = new Flee(fleeWeight);
        m_seekBehaviour = new Seek(seekWeight);
        m_separationBehaviour = new Separation(separationWeight);

        // Enemy init
        for (int i = 0; i < m_enemyCount; i++)
        {
            // Init and position setting
            var tempSprite = Instantiate(m_seekSprite, this.transform);
            Agent seeker = new Agent("Seeker" + i, tempSprite);
            float xPos = Random.Range(-100.0f, 100.0f);
            float yPos = Random.Range(-100.0f, 100.0f);
            seeker.SetPos(new Vector2(xPos, yPos));


            m_fleeBehaviour.SetObsticle(m_obsticles);

            m_seekBehaviour.SetTarget(m_player);

            // Each flee behaviour will need to hav its own obsticle I think
            // Adding behaviours
            seeker.AddBehaviour(m_fleeBehaviour);
            seeker.AddBehaviour(m_seekBehaviour);

            // Adding to list
            m_seekers.Add(seeker);
        }

        // Adding the sepeparation behavior, this has to wait till all the seekers have been initialized
        foreach (Agent seeker in m_seekers)
        {
            m_separationBehaviour.SetNeighbourhood(m_seekers);
            seeker.AddBehaviour(m_separationBehaviour);
        }
    }

    private void Update()
    {
        m_player.AgentUpdate();

        foreach (Agent currentSeeker in m_seekers)
        {
            currentSeeker.AgentUpdate();
        }

        m_fleeBehaviour.UpdateWeight(fleeWeight);
        m_seekBehaviour.UpdateWeight(seekWeight);
        m_separationBehaviour.UpdateWeight(separationWeight);
    }
}
