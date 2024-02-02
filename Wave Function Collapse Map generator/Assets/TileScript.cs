using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    int Identity;
    public List<int[]> AviableTiles;
    public int[] Edges;
    public bool collapsed;
    public int x;
    public int y;
    public Sprite empty;
    public Sprite up;
    public Sprite right;
    public Sprite bottom;
    public Sprite left;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        Edges = new int[4];
        AviableTiles = new List<int[]>
        {
            new int[]{0,0,0,0},
            new int[]{1,1,0,1},
            new int[]{1,1,1,0},
            new int[]{0,1,1,1},
            new int[]{1,0,1,1},
        };
       
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   public void DrawMap()
    {
        collapsed = true;
        int pickedArray = Random.Range(0, AviableTiles.Count);
        Edges = AviableTiles[pickedArray];
        pickPicture(pickedArray);
        for(int i=0; i < 4; i++)
        {
            Debug.Log(Edges[i]);
        }

    }
    void pickPicture(int pickedArray)
    {
        if (Edges[0] == 0)
        {
            if (Edges[1] == 0)
            {
                spriteRenderer.sprite = empty;
            }else if(Edges[1] == 1)
            {
                spriteRenderer.sprite = bottom;
            }
        }else if(Edges[0] == 1)
        {
            if (Edges[1] == 0)
            {
                spriteRenderer.sprite = left;
            }
            else if (Edges[2] == 0)
            {
                spriteRenderer.sprite = up;
            }else if(Edges[3] == 0)
            {
                spriteRenderer.sprite = right;
              
            }
        }
    }
}
