using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridMap : MonoBehaviour {


    public float horizontalSpacing;
    public float verticalSpacing;
    public GameManager gameManager;

    private Transform groundLayer;
    public Transform GroundLayer
    {
        get
        {
            if(groundLayer == null)
            {
                groundLayer = new GameObject("GroundLayer").transform;
                groundLayer.SetParent(transform);
            }
            return groundLayer;
        }
    }

    private int widthGrid;
    public int WidthGrid
    {
        get
        {
            return widthGrid;
        }
        set
        {
            widthGrid = value;
        }
    }

    private int heightGrid;
    public int HeightGrid
    {
        get
        {
            return heightGrid;
        }
        set
        {
            heightGrid = value;
        }
    }
    private int[,] groundGrid;
    private int[,] objectGrid;
    private PlayerController playerController;
    private List<GridObject> objectList;

	public void LoadLevel () {
        groundGrid = SampleLevelFloor();
        objectGrid = SampleObjectPosition();
        RenderGroundMap();
        PopulateMap();
	}

    private void RenderGroundMap()
    {
        SpriteRenderer groundTexture = ObjectFactory.GroundPrefab("GroundTexture");
        for (int i = 0; i < HeightGrid; i++)
        {
            for (int j = 0; j < WidthGrid; j++)
            {
                if (groundGrid[i, j] == 0)
                {
                    SpriteRenderer spriteRenderer = GameObject.Instantiate(groundTexture);
                    spriteRenderer.transform.position = new Vector3(j * horizontalSpacing, (HeightGrid- i) * verticalSpacing, 0);
                    spriteRenderer.transform.SetParent(GroundLayer);
                }
            }
        }
    }

    /// <summary>
    /// Places objects on map
    /// Uses a matrix as the level
    /// Runs once at start of Level
    /// </summary>
    private void PopulateMap()
    {
        objectList = new List<GridObject>();
        for (int i = 0; i < HeightGrid; i++)
        {
            for (int j = 0; j < WidthGrid; j++)
            {
                if (objectGrid[i, j] != 0)
                {
                    SpriteRenderer spriteRenderer = GameObject.Instantiate(ObjectFactory.ObjectPrefab( objectGrid[i,j]));
                    spriteRenderer.transform.position = new Vector3(j * horizontalSpacing, (HeightGrid - i) * verticalSpacing, 0);
                    GridObject gridObject = spriteRenderer.gameObject.AddComponent<GridObject>();
                    gridObject.x = j;
                    gridObject.y = i;
                    objectList.Add(gridObject);
                    //Add treat count to GameManager
                    if (objectGrid[i,j] == 2)
                    {
                        gameManager.TreatsLeft += 1;
                    }
                    //For player remove from grid and assign its gridmap value
                    if(objectGrid[i,j] == 9)
                    {
                        objectGrid[i, j] = 0;
                        playerController = spriteRenderer.GetComponent<PlayerController>();
                        playerController.gridMap = this;
                        gameManager.playerController = playerController;
                        objectList.Remove(gridObject);
                    }
                }

            }
        }
    }


    /// <summary>
    /// Use to destroy objects such as dog treats and other collectables
    /// </summary>
    /// <param name="position"></param>
    public void DestroyObjectAt(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / horizontalSpacing);
        int y = (HeightGrid - Mathf.RoundToInt(position.y / verticalSpacing));
        DestroyObjectAt(x, y);
    }

    private void DestroyObjectAt(int x, int y)
    {
        Debug.Log("Hit Destructable");
        int position = 0;
        bool objectDestroyed = false;
        for (int i = 0; i < objectList.Count; i++)
        {
            if(objectList[i].x == x && objectList[i].y == y)
            {
                //Check for a treat destroy
                if (objectGrid[y, x] == 2)
                {
                    //Reduce treat count
                    gameManager.TreatsLeft -= 1;
                    //Check for win condition
                    if(gameManager.TreatsLeft == 0)
                    {
                        gameManager.BeginWinGame();
                    }
                }

                objectDestroyed = true;
                objectList[i].gameObject.SetActive(false);
                objectGrid[y, x] = 0;
                position = i;
              

                break;
            }
        }

        if(objectDestroyed == true)
        {
            objectList.RemoveAt(position);
        }
    }

    // Gets an objects value from a world position
    public int GetObjectValue(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / horizontalSpacing);
        int y = (HeightGrid- Mathf.RoundToInt(position.y / verticalSpacing));
        return objectGrid[y, x]; //switch because x and y are inverted in matrix
    }

    private int[,] SampleLevelFloor()
    {
        /*
       0 - ground
       -1 - empty
       */
        WidthGrid = 7;
        HeightGrid = 5;
        int[,] grid = new int[5,7] {
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,-1 },
            {0,0,0,0,0,0,-1 },
        };
       
        return grid;
    }

    private int[,] SampleObjectPosition()
    {

        /*
        0 - nothing
        1 - obstacle
        2 - treat
        9 - player
        */

        int[,] grid = new int[,] {
            {1,1,1,1,1,1,1 },
            {1,0,0,0,0,0,1 },
            {1,0,9,1,2,0,1 },
            {1,0,0,0,0,1,0 },
            {1,1,1,1,1,1,0 },
        };

        return grid;
    }
}
