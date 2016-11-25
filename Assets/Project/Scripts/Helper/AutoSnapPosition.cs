using UnityEngine;
using System.Collections;

/// <summary>
/// Detector class for snaping to grid
/// Look at Editor/MenuAutoSnap
/// </summary>
public class AutoSnapPosition : MonoBehaviour {

	public void SnapPosition() {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x / 2.56f);
        position.x *= 2.56f;
        position.y = Mathf.Round(position.y / 2.56f);
        position.y *= 2.56f;
        transform.position = position;
    }
}
