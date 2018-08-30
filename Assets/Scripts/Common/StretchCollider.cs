using UnityEngine;
using System.Collections;

public class StretchCollider : MonoBehaviour {

    void OnEnable()
    {
        NGUITools.AddWidgetCollider(gameObject);
    }
}
