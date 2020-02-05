using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragTargeting : MonoBehaviour {
    public static SymbolCaptureCanvasController symbolController;
    public static TermCaptureCanvasController termController;
    public Vector3 offset;

    void Start() {
        symbolController = GameObject.Find("Canvas").GetComponent<SymbolCaptureCanvasController>(); 
        termController = GameObject.Find("Canvas").GetComponent<TermCaptureCanvasController>();
    }
   
    Vector3 mousePositionToWorldPosition() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
    }

    void OnMouseDown() {
        offset = transform.position - mousePositionToWorldPosition();
        if(symbolController!=null){ symbolController.setDraggingObject(true); }
        if (termController != null) {  termController.setDraggingObject(true); }
    }
    void OnMouseUp() {
        if (symbolController != null) { symbolController.setDraggingObject(false); }
        if (termController != null) { termController.setDraggingObject(false); }
    }

    void OnMouseDrag() {
        transform.position = Vector3.Lerp(transform.position, mousePositionToWorldPosition() + offset, 0.3f); 
    }

}
