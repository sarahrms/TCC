using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalingCanvasController : MonoBehaviour {
    private Vector3 frontCameraPosition, frontCameraRotation, topCameraPosition, topCameraRotation, rightCameraPosition, rightCameraRotation,
        leftCameraPosition, leftCameraRotation;
    public Camera frontCamera, topCamera, rightCamera, leftCamera;
    public Dropdown cameraDropdown;
    public Slider speedSlider;
    public InputField nome;
    public SignalingSystemController controller;

    void Start() {
        getInitialCameraPositions();
        setCameras();
    }

    void getInitialCameraPositions() {
        frontCameraPosition = frontCamera.gameObject.transform.position;
        frontCameraRotation = frontCamera.gameObject.transform.rotation.eulerAngles;
        topCameraPosition = topCamera.gameObject.transform.position;
        topCameraRotation = topCamera.gameObject.transform.rotation.eulerAngles;
        leftCameraPosition = leftCamera.gameObject.transform.position;
        leftCameraRotation = leftCamera.gameObject.transform.rotation.eulerAngles;
        rightCameraPosition = rightCamera.gameObject.transform.position;
        rightCameraRotation = rightCamera.gameObject.transform.rotation.eulerAngles;
    }

    void disableAllCameras() {
        frontCamera.enabled = false;
        frontCamera.gameObject.transform.position = frontCameraPosition;
        frontCamera.gameObject.transform.rotation = Quaternion.Euler(frontCameraRotation);

        topCamera.enabled = false;
        topCamera.gameObject.transform.position = topCameraPosition;
        topCamera.gameObject.transform.rotation = Quaternion.Euler(topCameraRotation);

        leftCamera.enabled = false;
        leftCamera.gameObject.transform.position = leftCameraPosition;
        leftCamera.gameObject.transform.rotation = Quaternion.Euler(leftCameraRotation);

        rightCamera.enabled = false;
        rightCamera.gameObject.transform.position = rightCameraPosition;
        rightCamera.gameObject.transform.rotation = Quaternion.Euler(rightCameraRotation);
    }

    void setCameras() {
        disableAllCameras();
        frontCamera.enabled = true;
    }

    public void changeCamera() {
        int option = cameraDropdown.value;
        disableAllCameras();
        switch (option) {
            case 0:
                frontCamera.enabled = true;
                break;
            case 1:
                rightCamera.enabled = true;
                break;
            case 2:
                leftCamera.enabled = true;
                break;
            case 3:
                topCamera.enabled = true;
                break;
        }
    }

    public void changeWord() {
        controller.loadWord(nome.text);
    }

    public void changeSpeed() {
        controller.setSpeed(speedSlider.value);
    }

    public void play() {
        controller.setSignalling();
    }
}
