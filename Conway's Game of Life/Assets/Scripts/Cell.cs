using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class Cell : MonoBehaviour
{
    public bool isCellAlive;
    private bool markedAlive, markedDead;
    private Renderer rend; 
    public Color aliveColour, deadColour;

    // Set the colour of the cell at the start according to starting state 
    private void Awake()
    {
        rend = GetComponent<Renderer>(); 
        float rand = Random.Range(0, 10);
        if (rand > 8)
            isCellAlive = true;
        else
            isCellAlive = false;
        rend.material.color = isCellAlive ? aliveColour : deadColour; 
    }

    // Mark a cell as dead or alive. This will nto change the state, just mark for later 
    public void MarkDead() { markedDead = true; }
    public void MarkAlive() { markedAlive = true; }

    // Update the state of the cell. The cell will be dead of arlive if it was marked previously
    public void UpdateCell()
    {
        if (markedAlive)
            ActivateCell();
        if (markedDead)
            DeactivateCell(); 
    }

    // These methods will kill and revive cells 
    public void ActivateCell()
    {
        markedAlive = false;
        markedDead = false;
        isCellAlive = true;

        rend.material.color = aliveColour; // Update the graphics 
    }

    public void DeactivateCell()
    {
        markedAlive = false;
        markedDead = true;
        isCellAlive = false;

        rend.material.color = deadColour; 
    }

    // Mouse pointers input handler. cell will switch state if clicked
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (isCellAlive)
                DeactivateCell();
            else
                ActivateCell(); 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCellAlive)
            DeactivateCell();
        else
            ActivateCell(); 
    }
}
