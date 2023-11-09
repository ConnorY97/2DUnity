using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConwaysManager : MonoBehaviour
{
    public int cellCount_X, cellCount_Y;
    public Cell[][] cells;
    public Cell cellPrefab;
    public Transform parent;

    public float updateTimer;
    public float updateDelay;

    int currentDay = 1;

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
            currentDay++;
            Debug.Log(currentDay + " days passed");
            UpdateCells();
            updateTimer = Time.time + updateDelay;
        }
    }
}
