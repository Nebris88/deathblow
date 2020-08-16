using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHeight : MonoBehaviour
{
    int lastChildCount = 0;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != lastChildCount)
        {
            lastChildCount = transform.childCount;
            GetComponent<RectTransform>().sizeDelta = new Vector2(0, lastChildCount * 30);
        }
    }
}
