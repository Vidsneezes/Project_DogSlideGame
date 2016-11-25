using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour {

	public SpriteRenderer SpawnObject(string objectName)
    {
        SpriteRenderer spriteRenderer = Resources.Load<SpriteRenderer>("Objects/" + objectName);
        return spriteRenderer;
    }
}
