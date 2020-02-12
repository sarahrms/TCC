using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHandPosition : MonoBehaviour {
    bool curveMovement = false;
    Vector3 offset = new Vector3(0, 90, 0);
    float lastXValue, lastYValue, lastZValue;
    public Transform positionAggregator, handTarget;
    public Button addButton;
    public GameObject positionPrefab;
    public Slider sliderX, sliderY, sliderZ;

    void Start(){
        StartCoroutine(init());
    }

    IEnumerator init() {
        yield return new WaitForSeconds(0.2f);
        positionPrefab = Resources.Load("HandPosition") as GameObject;
        
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
            if (curveMovement) {
                addOption(positionAggregator.GetChild(positionAggregator.childCount - 2).gameObject);
            }
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

    public void addCurveTrajectoryOptions() {
        curveMovement = true;
        for(int i=0; i<positionAggregator.childCount; i++) {
            GameObject position = positionAggregator.GetChild(i).gameObject;
            addOption(position);
        }
        removeOption(positionAggregator.GetChild(positionAggregator.childCount-1).gameObject);        
    }

    public void removeCurveTrajectoryOptions() {
        curveMovement = false;
        for (int i=0; i<positionAggregator.childCount; i++) {
            GameObject position = positionAggregator.GetChild(i).gameObject;
            removeOption(position);
        }
    }

    public void addOption(GameObject obj) {
        obj.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void removeOption(GameObject obj) {
        obj.transform.GetChild(3).gameObject.SetActive(false);
    }

}
