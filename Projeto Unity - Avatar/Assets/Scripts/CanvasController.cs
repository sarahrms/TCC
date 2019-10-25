using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
   public Camera frontCamera, topCamera, rightCamera, leftCamera;
   public Dropdown cameraDropdown;

   void Start() {
        setCameras();
   }
   void disableAllCameras() {
        frontCamera.enabled = false;
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
   }
   void setCameras() {
        disableAllCameras();
        frontCamera.enabled = true;
   }
   public void changeCamera() {
        int option = cameraDropdown.value;
        switch (option) {
            case 0:
                disableAllCameras();
                frontCamera.enabled = true;
                break;
            case 1:
                disableAllCameras();
                rightCamera.enabled = true;
                break;
            case 2:
                disableAllCameras();
                leftCamera.enabled = true;
                break;
            case 3:
                disableAllCameras();
                topCamera.enabled = true;
                break;
        }
   }


}
