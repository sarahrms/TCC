using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class CaptureSystemCanvasController : MonoBehaviour {
    public Transform frontCameraTransform, topCameraTransform, rightCameraTransform, leftCameraTransform;
    public Camera frontCamera, topCamera, rightCamera, leftCamera;
    public GameObject handConfigurationInterface, headConfigurationInterface, bodyConfigurationInterface, 
        movementDescriptionInterface, movementDynamicInteface, currentInterface; 
    public CaptureSystemController controller;
    public GROUP selectedGroup;
    public InputField idInputField;
    public Dropdown cameraDropdown, groupDropdown;

    void Start() {
        currentInterface = handConfigurationInterface;
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
        Debug.Log(controller.changeGroup(getId(), selectedGroup));
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
                setMovementDescriptionInterface(selectedGroup);
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
        currentInterface = handConfigurationInterface;
    }

    public void setHeadConfigurationInterface() {
        headConfigurationInterface.SetActive(true);
        currentInterface = headConfigurationInterface;
    }

    public void setBodyConfigurationInterface() {
        bodyConfigurationInterface.SetActive(true);
        currentInterface = bodyConfigurationInterface;
    }

    public void setMovementDescriptionInterface(GROUP selectedGroup) {
        movementDescriptionInterface.SetActive(true);

        if(selectedGroup == GROUP.FINGER_MOVEMENT){ 
            movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(false);
            killChildren(movementDescriptionInterface.transform.GetChild(2).gameObject);
            movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(true);
            currentInterface = movementDescriptionInterface.transform.GetChild(3).gameObject;
        }
        else { //hand movement interface
            movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(true);
            currentInterface = movementDescriptionInterface.transform.GetChild(2).gameObject;
            movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(false);
            killChildren(movementDescriptionInterface.transform.GetChild(3).gameObject);
        }
    }

    public void killChildren(GameObject obj) {
     /*   while(obj.transform.childCount > 1) {
             Destroy(obj.transform.GetChild(0).gameObject);
        }*/
    }
    public void setMovementDynamicInteface() {
        movementDynamicInteface.SetActive(true);
    }

    public int getId() {
        try{ 
            return Convert.ToInt32(idInputField.text);
        }
        catch {
            displayMessage();
            return 0;
        }
    }

    public void save() {
        if (controller.symbol != null) {
            controller.save(currentInterface);
        }
        else {
            displayMessage();
        }
    }

    public void setId() {
        if (controller.symbol != null) {
            controller.symbol.id = getId();
        }
    }

    public void displayMessage() {

    }
}
