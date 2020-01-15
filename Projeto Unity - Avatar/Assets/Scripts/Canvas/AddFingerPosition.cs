﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFingerPosition : MonoBehaviour {
    Vector3 offset = new Vector3(0, 120, 0);
    public Transform positionAggregator;
    public Button addButton;
    public GameObject positionPrefab;
    void Start(){
        positionPrefab = Resources.Load("FingerPosition") as GameObject;
    }

    public void addPosition() {
        if (positionAggregator.childCount < 3) {
            GameObject obj = Instantiate(positionPrefab);
            obj.transform.SetParent(positionAggregator);
            obj.transform.position = positionAggregator.GetChild(positionAggregator.childCount - 2).position - offset;
            obj.name = "Position " + positionAggregator.childCount.ToString();
            obj.GetComponent<Text>().text = "Posição " + positionAggregator.childCount.ToString();

            addButton.transform.position -= offset;
        }
        if (positionAggregator.childCount == 3) {
            addButton.gameObject.SetActive(false);
        }
    }
}
