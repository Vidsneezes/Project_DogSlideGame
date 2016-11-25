using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectFactory : MonoBehaviour {

    private static Dictionary<int, string> ObjectMap = new Dictionary<int, string>()
    {
        {1,"Wheels" },
        {2,"DogTreat" },
        {9,"Player" }
    };

	public static SpriteRenderer ObjectPrefab(string objectName)
    {
        SpriteRenderer spriteRenderer = Resources.Load<SpriteRenderer>("Objects/" + objectName);
        return spriteRenderer;
    }

    public static SpriteRenderer ObjectPrefab(int objectPositionValue)
    {
        SpriteRenderer spriteRenderer = Resources.Load<SpriteRenderer>("Objects/" + ObjectMap[objectPositionValue]);
        return spriteRenderer;
    }

    public static SpriteRenderer GroundPrefab(string groundName)
    {
        SpriteRenderer spriteRenderer = Resources.Load<SpriteRenderer>("Ground/" + groundName);
        return spriteRenderer;
    }
}
