using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {
    GameObject line1, line2;

    public void Start() {
        line1 = new GameObject();
        line1.AddComponent<LineRenderer>();
        line2 = new GameObject();
        line2.AddComponent<LineRenderer>();
    }
    public void draw() {
        Transform aggregator = GameObject.Find("MovementDescriptionInterface").transform.GetChild(2);
        for (int i = 0; i < aggregator.childCount; i++) {
            Transform child = aggregator.GetChild(i);
           // line1.GetComponent<LineRenderer>().po
        }
    }
}
