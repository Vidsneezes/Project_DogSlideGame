using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMap : MonoBehaviour {

    private int[,] groundFloor;
    private int[,] objects;

	// Use this for initialization
	private void Start () {
        groundFloor = SampleLevelFloor();
        objects = SampleObjectPosition();
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
