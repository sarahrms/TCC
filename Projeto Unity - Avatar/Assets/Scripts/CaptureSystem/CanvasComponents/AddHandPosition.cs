using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHandPosition : MonoBehaviour {
    Vector3 offset = new Vector3(0, 80, 0);
    public Transform positionAggregator, handTarget;
    public Button addButton;
    public GameObject positionPrefab;
    public Slider sliderX, sliderY, sliderZ;

    void Start(){
        positionPrefab = Resources.Load("HandPosition") as GameObject;
        positionAggregator.GetChild(0).GetComponent<SetHandPosition>().init();
        handTarget = GameObject.Find("mixamorig:RightHand - Target").transform;
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
        float initialSliderValue = 20;
        float currentSliderValue = sliderX.value;

        Vector3 rotation = handTarget.eulerAngles;
        rotation.x = currentSliderValue - initialSliderValue;
        handTarget.rotation = Quaternion.Euler(rotation);
    }

    public void rotacionarY() {
        float initialRotationYValue = -30;
        float initialSliderValue = 60;
        float currentSliderValue = sliderX.value;

        Vector3 rotation = handTarget.eulerAngles;
        rotation.y = (currentSliderValue - initialSliderValue) + initialRotationYValue;
        handTarget.rotation = Quaternion.Euler(rotation);
    }

    public void rotacionarZ() {
        int minValue = 0, maxValue = 40, initialValue = 20;


    }

}
