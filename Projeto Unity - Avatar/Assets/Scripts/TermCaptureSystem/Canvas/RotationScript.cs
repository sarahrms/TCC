using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationScript : MonoBehaviour {
    public bool rightHand;
    public Transform handTarget;
    public Slider sliderX, sliderY, sliderZ;
    float lastXValue, lastYValue, lastZValue;
    void Start() {
        StartCoroutine(init());
    }
    IEnumerator init() {
        yield return new WaitForSeconds(0.10f);
        handTarget = rightHand ? GameObject.Find("mixamorig:RightHand - Target").transform : GameObject.Find("mixamorig:LeftHand - Target").transform;
        lastXValue = sliderX.value;
        lastYValue = sliderY.value;
        lastZValue = rightHand? sliderZ.value : -sliderZ.value;
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
        float currentSliderValue = rightHand ? sliderZ.value : -sliderZ.value;
        handTarget.Rotate(0, 0, currentSliderValue - lastZValue);
        lastZValue = currentSliderValue;
    }
}
