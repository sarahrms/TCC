using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragTargeting : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    private GameObject target;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start() {
        ikScript = GameObject.Find("Avatar").GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();

        target = new GameObject();
        target.name = gameObject.name + " - target";
        target.transform.position = transform.position;
        target.transform.rotation = transform.rotation;
        target.transform.localScale = transform.localScale;

        switch (gameObject.name) {
            case "mixamorig:LeftUpLeg":
                ikScript.solver.leftThighEffector.target = target.transform;
                break;
            case "mixamorig:RightUpLeg":
                ikScript.solver.rightThighEffector.target = target.transform;
                break;

                //TO DO: PEGAR OUTROS COMPONENTES//
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    Vector3 mousePositionToWorldPosition() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
    }

    void OnMouseDown() {
        offset = transform.position - mousePositionToWorldPosition();
    }

    void OnMouseDrag() {
        target.transform.position = mousePositionToWorldPosition() + offset; 
        Debug.Log(gameObject.name);
    }
}
