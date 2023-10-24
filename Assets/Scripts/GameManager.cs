using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public SpriteRenderer m_playerSprite;
	public SpriteRenderer m_seekSprite;
	public SpriteRenderer m_obsticleSprite;
	public int m_enemyCount = 1;
	public int m_obsticleCount = 1;
	public static float SPEED = 50.0f;
	private Agent m_player;
	//private Agent m_obsticle;
	private List<Agent> m_seekers = new List<Agent>();
	private List<Agent> m_obsticles = new List<Agent>();
	private Seek m_seekBehavior = new Seek();
	private Flee m_fleeBehaviour = new Flee();
	//private List<Seek> m_seekBehaviours = new List<Seek>();

    private void Start()
    {
		// Agent init
		m_player = new Agent("Player", m_playerSprite);
		m_player.SetPos(new Vector2(10.0f, 10.0f));
		//m_obsticle = new Agent("Obsticle", m_obsticleSprite);
		//m_obsticle.SetPos(new Vector2(5.0f, 5.0f));

		// Set initial position of agents
		m_player.AgentUpdate();
		//m_obsticle.AgentUpdate();

		// Behavior init
		m_seekBehavior.SetTarget(m_player);
        
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

		m_fleeBehaviour.SetObsticle(m_obsticles);

        // Enemy init
        for (int i = 0; i < m_enemyCount; i++)
		{
			// Init and position setting
			var tempSprite = Instantiate(m_seekSprite, this.transform);
			Agent seeker = new Agent("Seeker" + i, tempSprite);
			float xPos = Random.Range(-100.0f, 100.0f);
			float yPos = Random.Range(-100.0f, 100.0f);
			seeker.SetPos(new Vector2(xPos,yPos));
			
			// Each flee behaviour will need to hav its own obsticle I think
			// Adding behaviours
			seeker.AddBehaviour(m_seekBehavior);
			seeker.AddBehaviour(m_fleeBehaviour);

			// Adding to list
			m_seekers.Add(seeker);
		}
    }

    private void Update()
    {
		//m_player.AgentUpdate();
		//m_obsticle.AgentUpdate();
		foreach (Agent currentSeeker in m_seekers)
		{
			currentSeeker.AgentUpdate();
		}
    }
}


// Conways game of life
/*
public class GameManager : MonoBehaviour
{
	public int cellCount_X, cellCount_Y;
	public Cell[][] cells;
	public Cell cellPrefab;
	public Transform parent;

	public float updateTimer;
	public float updateDelay; 

	private void createCells()
	{
		cells = new Cell[cellCount_X][];
		for (int i = 0; i < cellCount_X; i++)
		{
			cells[i] = new Cell[cellCount_Y];
			for (int j = 0; j < cellCount_Y; j++)
			{
				// Instantiate cells and store them 
				Cell cell = Instantiate(cellPrefab, new Vector2(i, j), Quaternion.identity, parent);
				cells[i][j] = cell; 
				cells[i][j].name = "cell{i}{j}"; 
			}
		}
	}

	private void UpdateCells()
	{
		for (int i = 0; i < cells.Length; i++)
		{
			for (int j = 0; j < cells[i].Length; j++)
			{
				// Find the numbers of live neighbours. Also don't forget to skip corner and edge cells. They will have fewer number of neighbours.
				int liveNeighbours = 0;

				// Check bottom, left
				if (i > 0 && j > 0 && cells[i - 1][j - 1].isCellAlive)
					liveNeighbours++;

				// Check bottom
				if (j > 0 && cells[i][j - 1].isCellAlive)
					liveNeighbours++;

				// Check bottom right
				if (i < cells.Length - 1 & j > 0 && cells[i + 1][j - 1].isCellAlive)
					liveNeighbours++;

				// Check for right 
				if (i > 0 && cells[i - 1][j].isCellAlive)
					liveNeighbours++;

				//check for left
				if (i < cells.Length - 1 && cells[i + 1][j].isCellAlive)
					liveNeighbours++;

				//check for top left
				if (i > 0 && j < cells[i].Length - 1 && cells[i - 1][j + 1].isCellAlive)
					liveNeighbours++;

				//check for top
				if (j < cells[i].Length - 1 && cells[i][j + 1].isCellAlive)
					liveNeighbours++;

				//check for top right
				if (i < cells.Length - 1 && j < cells[i].Length - 1 && cells[i + 1][j + 1].isCellAlive)
					liveNeighbours++;

				// Now after finding the neighbour, we can check rule to mark them dead or alive for next update
				// Rule 1: A live cell with 2 or 3 alive neighbouring cells survies 
				if (cells[i][j].isCellAlive && (liveNeighbours == 2 || liveNeighbours == 3))
					continue; 
				// Rule 2: A dead cell with 3 neighbours will revive 
				if (!cells[i][j].isCellAlive && liveNeighbours == 3)
				{
					cells[i][j].MarkAlive();
					continue; 
				}
				// Rule 3: All other cells die 
				cells[i][j].MarkDead(); 
			}
		}

        // All cells are marked so update all cells
        for (int i = 0; i < cells.Length; i++)
        {
            for (int j = 0; j < cells[i].Length; j++)
            {
				cells[i][j].UpdateCell(); 
            }
        }
	}

    private void Start()
    {
		updateTimer = Time.time + updateDelay;
		createCells(); 
    }

    private void Update()
    {
        if (updateTimer < Time.time)
        {
			Debug.Log("1 Day passed"); 
			UpdateCells();
			updateTimer = Time.time + updateDelay; 
        }
    }
}
*/