using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMap : MonoBehaviour {


    public float HorizontalSpacing;
    public float VerticalSpacing;

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

	private void Start () {
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
                    spriteRenderer.transform.position = new Vector3(j * HorizontalSpacing, (HeightGrid- i) * VerticalSpacing, 0);
                    spriteRenderer.transform.SetParent(GroundLayer);
                }
            }
        }
    }

    private void PopulateMap()
    {
        for (int i = 0; i < HeightGrid; i++)
        {
            for (int j = 0; j < WidthGrid; j++)
            {
                if (objectGrid[i, j] != 0)
                {
                    SpriteRenderer spriteRenderer = GameObject.Instantiate(ObjectFactory.ObjectPrefab( objectGrid[i,j]));
                    spriteRenderer.transform.position = new Vector3(j * HorizontalSpacing, (HeightGrid - i) * VerticalSpacing, 0);
                    if(objectGrid[i,j] == 9)
                    {
                        objectGrid[i, j] = 0;
                        playerController = spriteRenderer.GetComponent<PlayerController>();
                        playerController.GridMap = this;
                    }
                }

            }
        }
    }

    // Gets an objects value from a world position
    public int GetObjectValue(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / HorizontalSpacing);
        int y = (HeightGrid- Mathf.RoundToInt(position.y / VerticalSpacing));
        Debug.Log("X : " + x + "Y: " + y);
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
