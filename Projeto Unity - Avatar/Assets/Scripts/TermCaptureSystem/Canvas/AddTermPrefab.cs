using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTermPrefab : MonoBehaviour {
    Vector3 offset = new Vector3(0, 50, 0);
    public Transform positionAggregator;
    public Button addButton;
    public GameObject prefab;
    void Start() {
        prefab = Resources.Load("TermPrefab") as GameObject;
    }

    public void add() {
        if (positionAggregator.childCount < 6) {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(positionAggregator);
            obj.transform.position = positionAggregator.GetChild(positionAggregator.childCount - 2).position - offset;
            obj.name = "Term " + (positionAggregator.childCount - 1).ToString();
            obj.GetComponent<Text>().text = "Termo " + (positionAggregator.childCount - 1).ToString() + ":";
            addButton.transform.position -= offset;
        }
        if (positionAggregator.childCount == 6) {
            addButton.gameObject.SetActive(false);
        }
    }

}
