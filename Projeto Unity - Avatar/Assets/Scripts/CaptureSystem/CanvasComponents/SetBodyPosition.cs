using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SetBodyPosition : MonoBehaviour {
    public Transform rightShoulderCanvasComponent, leftShoulderCanvasComponent, spineCanvasComponent;
    public GameObject rightShoulderTarget, leftShoulderTarget, spineTarget;
    public Vector3 rightShoulderPosition, leftShoulderPosition, spinePosition;

    public void Start() {
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() {
        rightShoulderTarget = GameObject.Find("mixamorig:RightArm - Target");
        leftShoulderTarget = GameObject.Find("mixamorig:LeftArm - Target");
        spineTarget = GameObject.Find("mixamorig:Spine - Target");

        spineCanvasComponent = transform.GetChild(1);
        rightShoulderCanvasComponent = transform.GetChild(2);
        leftShoulderCanvasComponent = transform.GetChild(3);
    }
    public void setPosition() {
        leftShoulderPosition = leftShoulderTarget.transform.position;
        rightShoulderPosition = rightShoulderTarget.transform.position;
        spinePosition = spineTarget.transform.position;
        updateCanvas();
    }

    public void updateCanvas() {
        rightShoulderCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(rightShoulderPosition.x, 2).ToString();
        rightShoulderCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(rightShoulderPosition.y, 2).ToString();
        rightShoulderCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(rightShoulderPosition.z, 2).ToString();

        leftShoulderCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(leftShoulderPosition.x, 2).ToString();
        leftShoulderCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(leftShoulderPosition.y, 2).ToString();
        leftShoulderCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(leftShoulderPosition.z, 2).ToString();

        spineCanvasComponent.GetChild(0).GetChild(1).GetComponent<Text>().text = System.Math.Round(spinePosition.x, 2).ToString();
        spineCanvasComponent.GetChild(1).GetChild(1).GetComponent<Text>().text = System.Math.Round(spinePosition.y, 2).ToString();
        spineCanvasComponent.GetChild(2).GetChild(1).GetComponent<Text>().text = System.Math.Round(spinePosition.z, 2).ToString();
    }

}
