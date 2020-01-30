using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetHandPosition : MonoBehaviour {
    public Transform canvasPositionComponent, canvasRotationComponent;
    public GameObject handTarget;
    public Vector3 handPosition, handRotation;

    public void init() { 
        handTarget = GameObject.Find("mixamorig:RightHand - Target");
        canvasPositionComponent = transform.GetChild(0);
        canvasRotationComponent = transform.GetChild(1);
    }
    public void setPosition() {
        handPosition = handTarget.transform.position;
        handRotation = handTarget.transform.rotation.eulerAngles;
        updateCanvas();
    }

    public void updateCanvas() {
        canvasPositionComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(handPosition.x,2).ToString();
        canvasPositionComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(handPosition.y,2).ToString();
        canvasPositionComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(handPosition.z,2).ToString();

        canvasRotationComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(handRotation.x, 2).ToString();
        canvasRotationComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(handRotation.y, 2).ToString();
        canvasRotationComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(handRotation.z, 2).ToString();
    }
    
}
