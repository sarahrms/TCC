using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFingersPosition : MonoBehaviour {
    public Transform canvasComponent, indexTarget, middleTarget, pinkyTarget, ringTarget, thumbTarget;
    public Vector3 indexPosition, middlePosition, pinkyPosition, ringPosition, thumbPosition; 

    public void Start() {
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() { 
        indexTarget = GameObject.Find("mixamorig:RightHandIndex1 - Target").transform;
        middleTarget = GameObject.Find("mixamorig:RightHandMiddle1 - Target").transform;
        pinkyTarget = GameObject.Find("mixamorig:RightHandPinky1 - Target").transform;
        ringTarget = GameObject.Find("mixamorig:RightHandRing1 - Target").transform;
        thumbTarget = GameObject.Find("mixamorig:RightHandThumb1 - Target").transform;

        canvasComponent = transform.GetChild(0);
    }
    public void setPosition() {
        indexPosition = indexTarget.localPosition;
        middlePosition = middleTarget.localPosition;
        pinkyPosition = pinkyTarget.localPosition;
        ringPosition = ringTarget.localPosition;
        thumbPosition = thumbTarget.localPosition;

        updateCanvas();
    }

    public void updateCanvas() {
        canvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = thumbPosition.ToString(); 
        canvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = indexPosition.ToString();
        canvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = middlePosition.ToString();
        canvasComponent.GetChild(3).GetChild(1).GetComponent<Text>().text = ringPosition.ToString();
        canvasComponent.GetChild(4).GetChild(1).GetComponent<Text>().text = pinkyPosition.ToString();
    }
    
}
