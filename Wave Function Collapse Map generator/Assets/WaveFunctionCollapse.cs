using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class WaveFunctionCollapse : MonoBehaviour
{
    public float withd;
    public int DM;
    GameObject[,] Grid;
    public GameObject Tile;
    int amount;
     
    
    void Start()
    {
        DM = 10;
        amount = 0;
        
        Grid = new GameObject[DM, DM];
        withd = 0.8f;
        DrawLocation();
       
         
    }

    // Update is called once per frame
    void Update()
    {
         
        
            Evaluate();
        
    }
    void Collapse(int x, int y)
    {
        amount++;
        Grid[x, y].GetComponent<TileScript>().DrawMap();
        GameObject collapsing = Grid[x, y];
        //top
        if (y != DM-1)
        {
            TileScript topAdjacent = Grid[x, y + 1].GetComponent<TileScript>();
            if (!topAdjacent.collapsed)
            {
                for(int i =0; i< topAdjacent.AviableTiles.Count; i++)
                {
                    if(topAdjacent.AviableTiles[i][2] != collapsing.GetComponent<TileScript>().Edges[0])
                    {
                        topAdjacent.AviableTiles.Remove(topAdjacent.AviableTiles[i]);
                        i--;
                    }
                }
            }

        }
        //right
        if (x != DM - 1)
        {
            TileScript rightAdjacent = Grid[x+1, y].GetComponent<TileScript>();
            if (!rightAdjacent.collapsed)
            {
                for (int i = 0; i < rightAdjacent.AviableTiles.Count; i++)
                {
                    if (rightAdjacent.AviableTiles[i][3] != collapsing.GetComponent<TileScript>().Edges[1])
                    {
                        rightAdjacent.AviableTiles.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
        //unten
        if (y != 0)
        {
            TileScript bottomAdjacent = Grid[x, y-1].GetComponent<TileScript>();
            if (!bottomAdjacent.collapsed)
            {
                for (int i = 0; i < bottomAdjacent.AviableTiles.Count; i++)
                {
                    if (bottomAdjacent.AviableTiles[i][0] != collapsing.GetComponent<TileScript>().Edges[2])
                    {
                        bottomAdjacent.AviableTiles.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
        //left
        if (x != 0)
        {
            TileScript leftAdjacent = Grid[x - 1, y].GetComponent<TileScript>();
            if (!leftAdjacent.collapsed)
            {
                for (int i = 0; i < leftAdjacent.AviableTiles.Count; i++)
                {
                    if (leftAdjacent.AviableTiles[i][1] != collapsing.GetComponent<TileScript>().Edges[3])
                    {
                        leftAdjacent.AviableTiles.RemoveAt(i);
                        i--;
                    }
                }
            }

        }
    }

    void Evaluate()
    {
        if (amount < DM * DM)
        {
            List<GameObject> copyGrid = new();
            for (int i = 0; i < DM; i++)
            {
                for (int j = 0; j < DM; j++)
                {
                    if (!Grid[i, j].GetComponent<TileScript>().collapsed)
                    {
                        copyGrid.Add(Grid[i, j]);
                    }
                }
            }

            for (int y = 0; y < copyGrid.Count - 1; y++)
            {
                GameObject n1 = copyGrid[y];
                GameObject n2 = copyGrid[y + 1];
                if (n1.GetComponent<TileScript>().AviableTiles.Count > n2.GetComponent<TileScript>().AviableTiles.Count)
                {
                    GameObject temp = copyGrid[y];
                    copyGrid[y] = copyGrid[y + 1];
                    copyGrid[y + 1] = temp;
                    y = -1;
                }
            }

            Collapse(copyGrid[0].GetComponent<TileScript>().x, copyGrid[0].GetComponent<TileScript>().y);

        }
    }

    
   void DrawLocation()
    {
        for(int i=0; i < DM; i++)
        {
            for(int j = 0; j < DM; j++)
            {
                Grid[i, j] = Instantiate(Tile, new Vector3(i * withd, j * withd, 0f), Quaternion.identity);
                Grid[i, j].GetComponent<TileScript>().x = i;
                Grid[i, j].GetComponent<TileScript>().y = j;
            }
        }
    }
     

}
