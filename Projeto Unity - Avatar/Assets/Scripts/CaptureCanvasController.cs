using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class CaptureCanvasController : MonoBehaviour {
    public Transform frontCameraTransform, topCameraTransform, rightCameraTransform, leftCameraTransform;
    public Camera frontCamera, topCamera, rightCamera, leftCamera;
    public GameObject handConfigurationInterface, headConfigurationInterface, bodyConfigurationInterface, 
        movementDescriptionInterface, movementDynamicInteface; 
    public SignCaptureController controller;
    public GROUP selectedGroup;
    public InputField idInputField;
    public Symbol symbol;
    public Dropdown cameraDropdown, groupDropdown;

    void Start() {
        getInitialPositions();
        setCameras();
        addGroupOptions();
    }

    public void setDraggingObject(bool state) {
        frontCamera.GetComponent<CameraDrag>().setEnabled(!state);
        topCamera.GetComponent<CameraDrag>().setEnabled(!state);
        leftCamera.GetComponent<CameraDrag>().setEnabled(!state);
        rightCamera.GetComponent<CameraDrag>().setEnabled(!state);
    }

    void getInitialPositions() {
        frontCameraTransform = frontCamera.gameObject.transform;
        topCameraTransform = topCamera.gameObject.transform;
        leftCameraTransform = leftCamera.gameObject.transform;
        rightCameraTransform = rightCamera.gameObject.transform;
    }
    void disableAllCameras() {
        frontCamera.enabled = false;
        frontCamera.gameObject.transform.position = frontCameraTransform.position;
        frontCamera.gameObject.transform.rotation = frontCameraTransform.rotation;

        topCamera.enabled = false;
        topCamera.gameObject.transform.position = topCameraTransform.position;
        topCamera.gameObject.transform.rotation = topCameraTransform.rotation;

        leftCamera.enabled = false;
        leftCamera.gameObject.transform.position = leftCameraTransform.position;
        leftCamera.gameObject.transform.rotation = leftCameraTransform.rotation;

        rightCamera.enabled = false;
        rightCamera.gameObject.transform.position = rightCameraTransform.position;
        rightCamera.gameObject.transform.rotation = rightCameraTransform.rotation;
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

    void addGroupOptions() {
        foreach(GROUP group in Enum.GetValues(typeof(GROUP))) {
            String groupName = new CultureInfo("en-US", false).TextInfo.ToTitleCase(group.ToString().Replace('_', ' ').ToLower());
            groupDropdown.options.Add(new Dropdown.OptionData(groupName));
        }
        groupDropdown.value = 0;
    }

    public void changeSelectedGroup() {
        selectedGroup = (GROUP) Enum.GetValues(typeof(GROUP)).GetValue(groupDropdown.value);
        disableAllInterfaces();
        switch(controller.changeGroup(getId(), selectedGroup)){
            case TYPE.HAND_CONFIGURATION:
                setHandConfigurationInterface();
                break;
            case TYPE.HEAD_CONFIGURATION:
                setHeadConfigurationInterface();
                break;
            case TYPE.BODY_CONFIGURATION:
                setBodyConfigurationInterface();
                break;
            case TYPE.MOVEMENT_DESCRIPTION:
                setMovementDescriptionInterface();
                break;
            case TYPE.MOVEMENT_DYNAMIC:
                setMovementDynamicInteface();
                break;
        }
    }

    public void disableAllInterfaces(){
        handConfigurationInterface.SetActive(false);
        headConfigurationInterface.SetActive(false);
        bodyConfigurationInterface.SetActive(false);
        movementDescriptionInterface.SetActive(false);
        movementDynamicInteface.SetActive(false);
    }

    public void setHandConfigurationInterface() {
        handConfigurationInterface.SetActive(true);
    }

    public void setHeadConfigurationInterface() {
        headConfigurationInterface.SetActive(true);
    }

    public void setBodyConfigurationInterface() {
        bodyConfigurationInterface.SetActive(true);
    }

    public void setMovementDescriptionInterface() {
        movementDescriptionInterface.SetActive(true);
    }

    public void setMovementDynamicInteface() {
        movementDynamicInteface.SetActive(true);
    }

    public int getId() {
        return Convert.ToInt32(idInputField.text);
    }

    public void save() {
        controller.save();
    }
}
