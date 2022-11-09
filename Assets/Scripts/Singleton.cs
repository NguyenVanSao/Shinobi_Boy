using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T _instant = null;
    public static T instant = _instant;

    private void Awake() {
        if(_instant != null) {
            return;
        }

        var GMs = FindObjectsOfType<T>();
        _instant = GMs[0];

        if(GMs.Length > 1)
        {
            Destroy(this.gameObject);
        }

    }
}
