﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragTargeting : MonoBehaviour {
    public GameObject target;
    public Vector3 offset;
   
    Vector3 mousePositionToWorldPosition() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
    }

    void OnMouseDown() {
        offset = transform.position - mousePositionToWorldPosition();
    }
   
    void OnMouseDrag() {
        transform.position = mousePositionToWorldPosition() + offset; 
    }

    private void OnMouseUp() {
        transform.position = target.transform.position;
        Debug.Log(target.transform.position);
    }
}