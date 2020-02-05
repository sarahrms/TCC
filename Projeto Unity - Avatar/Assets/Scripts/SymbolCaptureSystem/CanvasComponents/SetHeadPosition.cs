using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHeadPosition : MonoBehaviour {
    public Transform canvasPositionComponent, canvasRotationComponent;
    public GameObject headTarget;
    public Vector3 headPosition, headRotation;

    public void init() {
        headTarget = GameObject.Find("mixamorig:Head - Target");
        canvasPositionComponent = transform.GetChild(0);
        canvasRotationComponent = transform.GetChild(1);
    }
    public void setPosition() {
        headPosition = headTarget.transform.position;
        headRotation = headTarget.transform.rotation.eulerAngles;
        updateCanvas();
    }

    public void updateCanvas() {
        canvasPositionComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(headPosition.x, 2).ToString();
        canvasPositionComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(headPosition.y, 2).ToString();
        canvasPositionComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(headPosition.z, 2).ToString();

        canvasRotationComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(headRotation.x, 2).ToString();
        canvasRotationComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(headRotation.y, 2).ToString();
        canvasRotationComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(headRotation.z, 2).ToString();
    }
}
