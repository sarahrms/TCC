﻿using System;
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
    public Dropdown cameraDropdown, groupDropdown, tipoDropdown;

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

    public void addGroupOptions() {
        Symbol.setTypeMap();
        groupDropdown.ClearOptions();
        List<GROUP> list;
        switch (tipoDropdown.value){
            case 0:
                list = Symbol.typeMap[TYPE.HAND_CONFIGURATION];
                break;
            case 1:
                list = Symbol.typeMap[TYPE.HEAD_CONFIGURATION];
                break;
            case 2:
                list = Symbol.typeMap[TYPE.BODY_CONFIGURATION];
                break;
            case 3:
                list = Symbol.typeMap[TYPE.MOVEMENT_DESCRIPTION];
                break;
            case 4:
                list = Symbol.typeMap[TYPE.MOVEMENT_DYNAMIC];
                break;
            default:
                list = new List<GROUP>();
                break;
        }
        foreach(GROUP group in list) {
            String groupName = new CultureInfo("en-US", false).TextInfo.ToTitleCase(group.ToString().Replace('_', ' ').ToLower());
            groupDropdown.options.Add(new Dropdown.OptionData(groupName));
        }
        groupDropdown.RefreshShownValue();
        changeSelectedGroup();
    }

    public void changeSelectedGroup() {
        selectedGroup = (GROUP)Enum.GetValues(typeof(GROUP)).GetValue(groupDropdown.value);
        controller.changeGroup(getId(), selectedGroup);
        controller.reset();
        disableAllInterfaces();
        switch (tipoDropdown.value) {
            case 0:
                setHandConfigurationInterface();
                break;
            case 1:
                setHeadConfigurationInterface();
                break;
            case 2:
                setBodyConfigurationInterface();
                break;
            case 3:
                setMovementDescriptionInterface(selectedGroup);
                break;
            case 4:
                setMovementDynamicInteface();
                break;
            default:
                break;
        }
    }

    public void disableAllInterfaces(){
        handConfigurationInterface.SetActive(false);
        headConfigurationInterface.SetActive(false);
        bodyConfigurationInterface.SetActive(false);
        movementDescriptionInterface.SetActive(false);
        movementDynamicInteface.SetActive(false);
        if(controller.avatarSetupScript!=null){
            controller.disableAllTargets();
        }
    }

   

    public void setHandConfigurationInterface() {
        handConfigurationInterface.SetActive(true);
        currentInterface = handConfigurationInterface;
        if (controller.avatarSetupScript != null) {
            controller.enableHandConfigurationTargets();     
        }
    }

    public void setHeadConfigurationInterface() {
        headConfigurationInterface.SetActive(true);
        currentInterface = headConfigurationInterface;
       /* if (controller.avatarSetupScript != null) {
            controller.enableHeadConfigurationTargets();
        }*/
    }

    public void setBodyConfigurationInterface() {
        bodyConfigurationInterface.SetActive(true);
        currentInterface = bodyConfigurationInterface;
        if (controller.avatarSetupScript != null) {
            controller.enableBodyConfigurationTargets();  
        }
    }

    public void setMovementDescriptionInterface(GROUP selectedGroup) {
        movementDescriptionInterface.SetActive(true);

        if(selectedGroup == GROUP.FINGER_MOVEMENT){ 
            movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(false);
            killChildren(movementDescriptionInterface.transform.GetChild(2).gameObject);
            movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(true);
            currentInterface = movementDescriptionInterface.transform.GetChild(3).gameObject;
            if (controller.avatarSetupScript != null) {
                controller.enableMovementConfigurationTargets(true);
            }
        }
        else { //hand movement interface
            movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(true);
            currentInterface = movementDescriptionInterface.transform.GetChild(2).gameObject;
            movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(false);
            killChildren(movementDescriptionInterface.transform.GetChild(3).gameObject);
            if (controller.avatarSetupScript != null) {
                controller.enableMovementConfigurationTargets(false);
            }
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
