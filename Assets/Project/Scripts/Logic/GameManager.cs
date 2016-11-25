using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GridMap gridMap;
    public PlayerController playerController;

    public float TreatsLeft
    {
        get
        {
            return treatsLeft;
        }
        set
        {
            treatsLeft = value;
            treatCountText.text = treatsLeft.ToString();
        }
    }

    private float treatsLeft;

    #region UI
    public Text treatCountText;
    public GameObject loadScreen;
    public GameObject winScreen;
    #endregion

    private void Start () {
        winScreen.gameObject.SetActive(false);
        TreatsLeft = 0;
        gridMap.gameManager = this;
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
        loadScreen.gameObject.SetActive(false);
        gridMap.LoadLevel();
    }
	
    public void BeginWinGame()
    {
        StartCoroutine(WinGameRoutine());
    }

    private IEnumerator WinGameRoutine()
    {
        //another effect delay
        yield return new WaitForSeconds(0.3f);
        playerController.enabled = false;
        winScreen.gameObject.SetActive(true);
    }
}
