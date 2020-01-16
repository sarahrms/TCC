using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHandPosition : MonoBehaviour {
    public Transform rightHandCanvasComponent, leftHandCanvasComponent;
    public GameObject rightHandTarget, leftHandTarget;
    public Vector3 rightHandPosition, leftHandPosition;
    public Vector3 rightHandRotation, leftHandRotation;

    public void Start() {
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() { 
        rightHandTarget = GameObject.Find("mixamorig:RightHand - Target");
        leftHandTarget = GameObject.Find("mixamorig:LeftHand - Target");

        rightHandCanvasComponent = transform.GetChild(0);
        leftHandCanvasComponent = transform.GetChild(1);
    }
    public void setPosition() {
        leftHandPosition = leftHandTarget.transform.position;
        rightHandPosition = rightHandTarget.transform.position;
        updateCanvas();
    }

    public void updateCanvas() {
        rightHandCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(rightHandPosition.x,2).ToString();
        rightHandCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(rightHandPosition.y,2).ToString();
        rightHandCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(rightHandPosition.z,2).ToString();

        leftHandCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(leftHandPosition.x,2).ToString();
        leftHandCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(leftHandPosition.y,2).ToString();
        leftHandCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(leftHandPosition.z,2).ToString();
    }
    
}
