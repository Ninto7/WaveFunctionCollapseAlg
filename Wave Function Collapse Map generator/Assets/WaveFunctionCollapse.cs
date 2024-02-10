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
        //select a random tile of the aviable ones
        Grid[x, y].GetComponent<TileScript>().DrawMap();
        GameObject collapsing = Grid[x, y];
        //top
        if (y != DM-1)
        {
            //if top tile exists and is not collapsed
            TileScript topAdjacent = Grid[x, y + 1].GetComponent<TileScript>();
            if (!topAdjacent.collapsed)
            {
                //go through all possible tiles
                for(int i =0; i< topAdjacent.AviableTiles.Count; i++)
                {
                    //remove tiles that dont fit anymore
                    if(topAdjacent.AviableTiles[i][2] != collapsing.GetComponent<TileScript>().Edges[0])
                    {
                        topAdjacent.AviableTiles.Remove(topAdjacent.AviableTiles[i]);
                        //go back in the list once
                        i--;
                    }
                }
            }

        }
        //right
        if (x != DM - 1)
        {
            //if top tile exists and is not collapsed
            TileScript rightAdjacent = Grid[x+1, y].GetComponent<TileScript>();
            if (!rightAdjacent.collapsed)
            {
                //go through all possible tiles
                for (int i = 0; i < rightAdjacent.AviableTiles.Count; i++)
                {
                    //remove tiles that dont fit anymore
                    if (rightAdjacent.AviableTiles[i][3] != collapsing.GetComponent<TileScript>().Edges[1])
                    {
                        rightAdjacent.AviableTiles.RemoveAt(i);
                        //go back in the list once
                        i--;
                    }
                }
            }

        }
        //unten
        if (y != 0)
        {
            //if top tile exists and is not collapsed
            TileScript bottomAdjacent = Grid[x, y-1].GetComponent<TileScript>();
            if (!bottomAdjacent.collapsed)
            {
                //go through all possible tiles
                for (int i = 0; i < bottomAdjacent.AviableTiles.Count; i++)
                {
                    //remove tiles that dont fit anymore
                    if (bottomAdjacent.AviableTiles[i][0] != collapsing.GetComponent<TileScript>().Edges[2])
                    {
                        bottomAdjacent.AviableTiles.RemoveAt(i);
                        //go back in the list once
                        i--;
                    }
                }
            }

        }
        //left
        if (x != 0)
        {
            //if top tile exists and is not collapsed
            TileScript leftAdjacent = Grid[x - 1, y].GetComponent<TileScript>();
            if (!leftAdjacent.collapsed)
            {
                //go through all possible tiles
                for (int i = 0; i < leftAdjacent.AviableTiles.Count; i++)
                {
                    //remove tiles that dont fit anymore
                    if (leftAdjacent.AviableTiles[i][1] != collapsing.GetComponent<TileScript>().Edges[3])
                    {
                        leftAdjacent.AviableTiles.RemoveAt(i);
                        //go back in the list once
                        i--;
                    }
                }
            }

        }
    }

    void Evaluate()
    {
        //if not all tiles are set
        if (amount < DM * DM)
        {
            List<GameObject> copyGrid = new();
            //add all not collapsed tiles to a List
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
            //sort algorith based on aviable tiles amount
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
            //collapse the tile with the least possible tiles
            Collapse(copyGrid[0].GetComponent<TileScript>().x, copyGrid[0].GetComponent<TileScript>().y);

        }
    }

    
   void DrawLocation()
    {
        //create a simple grid of tiles
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
