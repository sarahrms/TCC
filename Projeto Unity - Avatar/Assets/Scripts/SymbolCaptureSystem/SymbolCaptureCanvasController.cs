using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class SymbolCaptureCanvasController : MonoBehaviour {
    public Vector3 frontCameraPosition, frontCameraRotation, topCameraPosition, topCameraRotation, rightCameraPosition, rightCameraRotation,
        leftCameraPosition, leftCameraRotation;
    public Camera frontCamera, topCamera, rightCamera, leftCamera;
    public GameObject handConfigurationInterface, headConfigurationInterface, bodyConfigurationInterface, 
        movementDescriptionInterface, currentInterface; 
    public SymbolCaptureSystemController controller;
    public TYPE selectedType;
    public GROUP selectedGroup;
    public InputField idInputField;
    public Dropdown cameraDropdown, groupDropdown, tipoDropdown;
    public Slider rotacaoX, rotacaoY, rotacaoZ;

    void Start() {
        StartCoroutine(init());
    }
    IEnumerator init() {
        yield return new WaitForSeconds(0.4f);
        currentInterface = handConfigurationInterface;
        getInitialCameraPositions();
        setCameras();
        controller.disableAllTargets();
        addGroupOptions();
    }

    public void setDraggingObject(bool state) {
        frontCamera.GetComponent<CameraDrag>().setEnabled(!state);
        leftCamera.GetComponent<CameraDrag>().setEnabled(!state);
        rightCamera.GetComponent<CameraDrag>().setEnabled(!state);
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

    public void addGroupOptions() {
        groupDropdown.ClearOptions();
        List<GROUP> list;
        switch (tipoDropdown.value){
            case 0:
                selectedType = TYPE.HAND_CONFIGURATION;               
                break;
            case 1:
                selectedType = TYPE.FACE_CONFIGURATION;
                break;
            case 2:
                selectedType = TYPE.BODY_CONFIGURATION;
                break;
            case 3:
                selectedType = TYPE.MOVEMENT_CONFIGURATION;
                break;
            case 4:
                selectedType = TYPE.MOVEMENT_DYNAMIC;
                break;
            default:
                break;
        }
        list = Symbol.typeMap[selectedType];
        foreach (GROUP group in list) {
            String groupName = new CultureInfo("en-US", false).TextInfo.ToTitleCase(group.ToString().Replace('_', ' ').ToLower());
            groupDropdown.options.Add(new Dropdown.OptionData(groupName));
        }
        groupDropdown.RefreshShownValue();
        changeSelectedGroup();
    }

    public void changeSelectedGroup() {
        selectedGroup = Symbol.typeMap[selectedType][groupDropdown.value];
        controller.changeGroup(getId(), selectedGroup);
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
            default:
                break;
        }
        controller.reset();
    }

    public void disableAllInterfaces(){
        handConfigurationInterface.SetActive(false);
        headConfigurationInterface.SetActive(false);
        bodyConfigurationInterface.SetActive(false);
        movementDescriptionInterface.SetActive(false);
    }

    public void setHandConfigurationInterface() {
        handConfigurationInterface.SetActive(true);
        currentInterface = handConfigurationInterface;
        if (controller.avatarSetupScript != null) {
            controller.disableAllTargets();
            controller.enableHandConfigurationTargets();     
        }
    }

    public void setHeadConfigurationInterface() {
        headConfigurationInterface.SetActive(true);
        currentInterface = headConfigurationInterface;
        if (controller.avatarSetupScript != null) {
            controller.disableAllTargets();
        }
    }

    public void setBodyConfigurationInterface() {
        bodyConfigurationInterface.SetActive(true);
        currentInterface = bodyConfigurationInterface;
        if (controller.avatarSetupScript != null) {
            controller.disableAllTargets();
            controller.enableBodyConfigurationTargets();  
        }
    }

    public void setMovementDescriptionInterface(GROUP selectedGroup) {
        movementDescriptionInterface.SetActive(true);

        switch(selectedGroup){
            case GROUP.FINGER_MOVEMENT: { 
                movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(true); //finger
                currentInterface = movementDescriptionInterface.transform.GetChild(2).gameObject;

                movementDescriptionInterface.transform.GetChild(1).gameObject.SetActive(false);//hand
                killChildren(movementDescriptionInterface.transform.GetChild(1).GetChild(1).gameObject);

                movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(false); //head
                killChildren(movementDescriptionInterface.transform.GetChild(3).GetChild(1).gameObject);

                break;
            }
            case GROUP.HEAD: { 
                movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(true); //head
                currentInterface = movementDescriptionInterface.transform.GetChild(3).gameObject;

                movementDescriptionInterface.transform.GetChild(1).gameObject.SetActive(false);//hand
                killChildren(movementDescriptionInterface.transform.GetChild(1).GetChild(1).gameObject);

                movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(false); //finger
                killChildren(movementDescriptionInterface.transform.GetChild(2).GetChild(1).gameObject);

                break;
            }
            default: { //hand movement interface
                movementDescriptionInterface.transform.GetChild(1).gameObject.SetActive(true); //hand
                currentInterface = movementDescriptionInterface.transform.GetChild(1).gameObject;

                if (selectedGroup.ToString().Contains("CURVE") || selectedGroup.ToString().Contains("CIRCLE")) {
                    movementDescriptionInterface.transform.GetChild(1).GetComponent<AddHandPosition>().addCurveTrajectoryOptions();
                    movementDescriptionInterface.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
                }
                else {
                    movementDescriptionInterface.transform.GetChild(1).GetComponent<AddHandPosition>().removeCurveTrajectoryOptions();
                    movementDescriptionInterface.transform.GetChild(1).GetChild(3).gameObject.SetActive(false);
                }              

                movementDescriptionInterface.transform.GetChild(2).gameObject.SetActive(false); //finger
                killChildren(movementDescriptionInterface.transform.GetChild(2).GetChild(1).gameObject);

                movementDescriptionInterface.transform.GetChild(3).gameObject.SetActive(false); //head
                killChildren(movementDescriptionInterface.transform.GetChild(3).GetChild(1).gameObject);

                break;
            }
        }
        if (controller.avatarSetupScript != null) {
            controller.disableAllTargets();
            controller.enableMovementConfigurationTargets(selectedGroup);
        }
    }

    public void killChildren(GameObject obj) {
       for(int i=1; i<obj.transform.childCount; i++) { 
             Destroy(obj.transform.GetChild(i).gameObject);
       }
    }

    public int getId() {
        try{ 
            return Convert.ToInt32(idInputField.text);
        }
        catch {
            return 0;
        }
    }

    public void save() {
        if (controller.symbol != null) {
            controller.save(currentInterface);
        }
    }

    public void setId() {
        if (controller.symbol != null) {
            controller.symbol.id = getId();
        }
    }

}
