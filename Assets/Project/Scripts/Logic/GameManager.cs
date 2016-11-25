using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GridMap gridMap;

    public float TreatsLeft
    {
        get
        {
            return treatsLeft;
        }
        set
        {
            treatsLeft = value;
        }
    }
    private float treatsLeft;

	private void Start () {
        TreatsLeft = 0;
        StartCoroutine(LevelEffectDelay());
	}

    /// <summary>
    /// Add this to create a short mandetory break between levels
    /// *Game Design creates anticipation
    /// </summary>
    /// <returns></returns>
    private IEnumerator LevelEffectDelay()
    {
        yield return new WaitForSeconds(0.5f);
        gridMap.LoadLevel();
    }
	
    public void BeginWinGame()
    {

    }

    private IEnumerator WinGameRoutine()
    {
        //another effect delay
        yield return new WaitForSeconds(0.3f);
    }
}
