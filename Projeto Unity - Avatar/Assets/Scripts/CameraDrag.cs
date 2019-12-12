using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour {
    float speed = 5.0f;
    public Transform target;
    void Update() {
        if (Input.GetMouseButton(1)) {
            transform.LookAt(target);
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
        }
    }



    Vector3 lastDragPosition;

    void Update() {
        UpdateDrag();
    }

    void UpdateDrag() {
        if (Input.GetMouseButtonDown(2))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(2)) {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * 0.25f);
            lastDragPosition = Input.mousePosition;
        }
    }
}
