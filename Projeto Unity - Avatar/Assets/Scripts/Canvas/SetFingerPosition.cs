using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFingerPosition : MonoBehaviour {
    public Transform rightHandCanvasComponent, leftHandCanvasComponent;
    public GameObject rightIndexTarget, rightMiddleTarget, rightPinkyTarget, rightRingTarget, rightThumbTarget,
        leftIndexTarget, leftMiddleTarget, leftPinkyTarget, leftRingTarget, leftThumbTarget;
    public Vector3 rightIndexPosition, rightMiddlePosition, rightPinkyPosition, rightRingPosition, rightThumbPosition, 
        leftIndexPosition, leftMiddlePosition, leftPinkyPosition, leftRingPosition, leftThumbPosition; 

    public void Start() {
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() { 
        rightIndexTarget = GameObject.Find("mixamorig:RightHandIndex1 - Target");
        rightMiddleTarget = GameObject.Find("mixamorig:RightHandMiddle1 - Target");
        rightPinkyTarget = GameObject.Find("mixamorig:RightHandPinky1 - Target");
        rightRingTarget = GameObject.Find("mixamorig:RightHandRing1 - Target");
        rightThumbTarget = GameObject.Find("mixamorig:RightHandThumb1 - Target");

        leftIndexTarget = GameObject.Find("mixamorig:LeftHandIndex1 - Target");
        leftMiddleTarget = GameObject.Find("mixamorig:LeftHandMiddle1 - Target");
        leftPinkyTarget = GameObject.Find("mixamorig:LeftHandPinky1 - Target");
        leftRingTarget = GameObject.Find("mixamorig:LeftHandRing1 - Target");
        leftThumbTarget = GameObject.Find("mixamorig:LefttHandThumb1 - Target");

        rightHandCanvasComponent = transform.GetChild(0);
        leftHandCanvasComponent = transform.GetChild(1);
    }
    public void setPosition() {
        rightIndexPosition = rightIndexTarget.transform.localPosition;
        rightMiddlePosition = rightMiddleTarget.transform.localPosition;
        rightPinkyPosition = rightPinkyTarget.transform.localPosition;
        rightRingPosition = rightRingTarget.transform.localPosition;
        rightThumbPosition = rightThumbTarget.transform.localPosition;

        leftIndexPosition = rightIndexTarget.transform.localPosition;
        leftMiddlePosition = rightMiddleTarget.transform.localPosition;
        leftPinkyPosition = rightPinkyTarget.transform.localPosition;
        leftRingPosition = rightRingTarget.transform.localPosition;
        leftThumbPosition = rightThumbTarget.transform.localPosition;

        updateCanvas();
    }

    public void updateCanvas() {
        rightHandCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = rightIndexPosition.ToString();
        rightHandCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = rightMiddlePosition.ToString();
        rightHandCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = rightPinkyPosition.ToString();
        rightHandCanvasComponent.GetChild(3).GetChild(1).GetComponent<Text>().text = rightRingPosition.ToString();
        rightHandCanvasComponent.GetChild(4).GetChild(1).GetComponent<Text>().text = rightThumbPosition.ToString();

        leftHandCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = leftIndexPosition.ToString();
        leftHandCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = leftMiddlePosition.ToString();
        leftHandCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = leftPinkyPosition.ToString();
        leftHandCanvasComponent.GetChild(3).GetChild(1).GetComponent<Text>().text = leftRingPosition.ToString();
        leftHandCanvasComponent.GetChild(4).GetChild(1).GetComponent<Text>().text = leftThumbPosition.ToString();
    }
    
}
