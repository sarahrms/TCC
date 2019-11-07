using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragTargeting : MonoBehaviour {
    public Transform target;
    public Vector3 offset;
   
    Vector3 mousePositionToWorldPosition() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(target.transform.position).z));
    }

    void OnMouseDown() {
        offset = target.transform.position - mousePositionToWorldPosition();
    }
   
    void OnMouseDrag() {
        target.transform.position = mousePositionToWorldPosition() + offset; 
    }

    private void OnMouseUp() {
        target.transform.position = transform.position;
    }
}
