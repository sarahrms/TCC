using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHeadPosition : MonoBehaviour {
    Vector3 offset = new Vector3(0, 80, 0);
    public Transform positionAggregator;
    public Button addButton;
    public GameObject positionPrefab;
    public Slider sliderX, sliderY, sliderZ;

    void Start() {
        positionPrefab = Resources.Load("HeadPosition") as GameObject;
        positionAggregator.GetChild(0).GetComponent<SetHeadPosition>().init();
    }

    public void addPosition() {
        if (positionAggregator.childCount < 5) {
            GameObject obj = Instantiate(positionPrefab);
            obj.transform.SetParent(positionAggregator);
            obj.transform.position = positionAggregator.GetChild(positionAggregator.childCount - 2).position - offset;
            obj.name = "Position " + positionAggregator.childCount.ToString();
            obj.GetComponent<Text>().text = "Configuração " + positionAggregator.childCount.ToString() + ":";
            obj.GetComponent<SetHeadPosition>().init();
            addButton.transform.position -= offset;
        }
        if (positionAggregator.childCount == 5) {
            addButton.gameObject.SetActive(false);
        }
    }

    public void rotacionarX() { }

    public void rotacionarY() { }

    public void rotacionarZ() { }

}