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
}
