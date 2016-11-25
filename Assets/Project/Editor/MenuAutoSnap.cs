using UnityEngine;
using UnityEditor;
using System.Collections;


/// <summary>
/// Editor Menu to snap all objects to 2.56 grid
/// </summary>
public class MenuAutoSnap : MonoBehaviour {

    [MenuItem("Helpers/AutoSnap")]
    public static void AutoSnapAll()
    {
        AutoSnapPosition[] pos = GameObject.FindObjectsOfType<AutoSnapPosition>();
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i].SnapPosition();
        }
    }
}
