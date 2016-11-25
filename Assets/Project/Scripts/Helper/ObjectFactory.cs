using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour {

	public static SpriteRenderer SpawnObjectPrefab(string objectName)
    {
        SpriteRenderer spriteRenderer = Resources.Load<SpriteRenderer>("Objects/" + objectName);
        return spriteRenderer;
    }

    public static SpriteRenderer SpawnGroundPrefab(string groundName)
    {
        SpriteRenderer spriteRenderer = Resources.Load<SpriteRenderer>("Ground/" + groundName);
        return spriteRenderer;
    }
}
