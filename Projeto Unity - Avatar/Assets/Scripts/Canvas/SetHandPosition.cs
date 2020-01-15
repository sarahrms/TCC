using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHandPosition : MonoBehaviour {
    public DrawLines drawLinesScript;
    public Transform rightHandCanvasComponent, leftHandCanvasComponent;
    public GameObject rightHandTarget, leftHandTarget;
    public Vector3 rightHandPosition, leftHandPosition;
    public Vector3 rightHandRotation, leftHandRotation;

    public void Start() {
        drawLinesScript = GameObject.Find("HandMovementDescriptionInterface").GetComponent<DrawLines>();
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() { 
        rightHandTarget = GameObject.Find("mixamorig:RightHand - target");
        leftHandTarget = GameObject.Find("mixamorig:LeftHand - target");

        rightHandCanvasComponent = transform.GetChild(0);
        leftHandCanvasComponent = transform.GetChild(1);
    }
    public void setPosition() {
        leftHandPosition = leftHandTarget.transform.position;
        rightHandPosition = rightHandTarget.transform.position;
        updateCanvas();
        updateLines();
    }

    public void updateLines() {
        drawLinesScript.draw();
    }

    public void updateCanvas() {
        rightHandCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = rightHandPosition.x.ToString();
        rightHandCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = rightHandPosition.y.ToString();
        rightHandCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = rightHandPosition.z.ToString();

        leftHandCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = leftHandPosition.x.ToString();
        leftHandCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = leftHandPosition.y.ToString();
        leftHandCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = leftHandPosition.z.ToString();
    }
    
}
