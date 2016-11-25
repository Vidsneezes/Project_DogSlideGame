using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMap : MonoBehaviour {


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
    private int widthGrid;
    private int heightGrid;

	// Use this for initialization
	private void Start () {
        groundGrid = SampleLevelFloor();
        objectGrid = SampleObjectPosition();
	}

    private void RenderGroundMap()
    {
        SpriteRenderer groundTexture = ObjectFactory.SpawnGroundPrefab("Ground");
        for (int i = 0; i < heightGrid; i++)
        {
            for (int j = 0; j < widthGrid; j++)
            {

            }
        }
    }
	
	// Update is called once per frame
	private void Update () {
	
	}


    private int[,] SampleLevelFloor()
    {
        /*
       0 - ground
       -1 - empty
       */

        int[,] grid = new int[,] {
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
