using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHandPosition : MonoBehaviour {
    Vector3 offset = new Vector3(0, 80, 0);
    float lastXValue, lastYValue, lastZValue;
    public Transform positionAggregator, handTarget;
    public Button addButton;
    public GameObject positionPrefab;
    public Slider sliderX, sliderY, sliderZ;

    void Start(){
        positionPrefab = Resources.Load("HandPosition") as GameObject;
        positionAggregator.GetChild(0).GetComponent<SetHandPosition>().init();
        handTarget = GameObject.Find("mixamorig:RightHand - Target").transform;
        lastXValue = sliderX.value;
        lastYValue = sliderY.value;
        lastZValue = sliderZ.value;

    }

    public void addPosition() {
        if (positionAggregator.childCount < 5) {
            GameObject obj = Instantiate(positionPrefab);
            obj.transform.SetParent(positionAggregator);
            obj.transform.position = positionAggregator.GetChild(positionAggregator.childCount - 2).position - offset;
            obj.name = "Position " + positionAggregator.childCount.ToString();
            obj.GetComponent<Text>().text = "Configuração " + positionAggregator.childCount.ToString() + ":";
            obj.GetComponent<SetHandPosition>().init();
            addButton.transform.position -= offset;
        }
        if (positionAggregator.childCount == 5) {
            addButton.gameObject.SetActive(false);
        }
    }

    public void rotacionarX() {
        float currentSliderValue = sliderX.value;
        handTarget.Rotate(currentSliderValue - lastXValue, 0, 0);
        lastXValue = currentSliderValue;
    }

    public void rotacionarY() {
        float currentSliderValue = sliderY.value;
        handTarget.Rotate(0, currentSliderValue - lastYValue, 0);
        lastYValue = currentSliderValue;
    }

    public void rotacionarZ() {        
        float currentSliderValue = sliderZ.value;
        handTarget.Rotate(0, 0, currentSliderValue - lastZValue);
        lastZValue = currentSliderValue;
    }

}
