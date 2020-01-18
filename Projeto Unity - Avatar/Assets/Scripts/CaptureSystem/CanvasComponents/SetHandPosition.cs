using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetHandPosition : MonoBehaviour {
    public Transform canvasComponent;
    public GameObject handTarget;
    public Vector3 handPosition, handRotation;

    public void Start() {
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() { 
        handTarget = GameObject.Find("mixamorig:RightHand - Target");
        canvasComponent = transform.GetChild(0);
    }
    public void setPosition() {
        handPosition = handTarget.transform.position;
        handRotation = handTarget.transform.rotation.eulerAngles;
        updateCanvas();
    }

    public void updateCanvas() {
        canvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(handPosition.x,2).ToString();
        canvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(handPosition.y,2).ToString();
        canvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(handPosition.z,2).ToString();

        canvasComponent.GetChild(3).GetChild(1).GetComponent<Text>().text = System.Math.Round(handRotation.x, 2).ToString();
        canvasComponent.GetChild(4).GetChild(1).GetComponent<Text>().text = System.Math.Round(handRotation.y, 2).ToString();
        canvasComponent.GetChild(5).GetChild(1).GetComponent<Text>().text = System.Math.Round(handRotation.z, 2).ToString();
    }
    
}
