using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SignalingSystemCanvasController : MonoBehaviour {
    public Transform frontCameraTransform, topCameraTransform, rightCameraTransform, leftCameraTransform;
    public Camera frontCamera, topCamera, rightCamera, leftCamera;

    public SignalingSystemController controller;
    public GROUP selectedGroup;
    public Dropdown cameraDropdown, maoDireitaDropdown, maoDireitaFileDropdown, 
        maoEsquerdaDropdown, maoEsquerdaFileDropdown;

    void Start() {
        getInitialPositions();
        setCameras();
        changeFileOptionsMaoEsquerda();
        changeFileOptionsMaoDireita();
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

    public void changeFileOptionsMaoDireita() {
        FileInfo[] fileInfo;
        DirectoryInfo levelDirectoryPath = getHandFilePath(maoDireitaDropdown);
        fileInfo = levelDirectoryPath.GetFiles("*.json*", SearchOption.AllDirectories);
        maoDireitaFileDropdown.ClearOptions();
        maoDireitaFileDropdown.options.Add(new Dropdown.OptionData("Nenhuma"));
        foreach (FileInfo file in fileInfo) {
            if (!file.Extension.Contains("meta")) { 
                maoDireitaFileDropdown.options.Add(new Dropdown.OptionData(file.Name.Remove(file.Name.IndexOf(file.Extension))));  
            }
        }
        maoDireitaFileDropdown.RefreshShownValue();
    }

    public void changeFileOptionsMaoEsquerda() {
        FileInfo[] fileInfo;
        DirectoryInfo levelDirectoryPath = getHandFilePath(maoEsquerdaDropdown);        
        fileInfo = levelDirectoryPath.GetFiles("*.json*", SearchOption.AllDirectories);
        maoEsquerdaFileDropdown.ClearOptions();
        maoEsquerdaFileDropdown.options.Add(new Dropdown.OptionData("Nenhuma"));
        foreach (FileInfo file in fileInfo) {
            if (!file.Extension.Contains("meta")) {
                maoEsquerdaFileDropdown.options.Add(new Dropdown.OptionData(file.Name.Remove(file.Name.IndexOf(file.Extension))));    
            }
        }
        maoEsquerdaFileDropdown.RefreshShownValue();
    }

    public DirectoryInfo getHandFilePath(Dropdown maoDropdown) {
        DirectoryInfo levelDirectoryPath;
        switch (maoDropdown.value) {
            case 0:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Index");
                break;
            case 1:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Index_Middle");
                break;
            case 2:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Index_Middle_Thumb");
                break;
            case 3:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Four_Fingers");
                break;
            case 5:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Five_Fingers");
                break;
            case 6:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Baby_Finger");
                break;
            case 7:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Ring_Finger");
                break;
            case 8:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Middle_Finger");
                break;
            case 9:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Index_Thumb");
                break;
            case 10:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Thumb");
                break;
            default:
                levelDirectoryPath = new DirectoryInfo("");
                break;
        }
        return levelDirectoryPath;
    }

    public void loadRightHandConfiguration() {
        if(maoDireitaFileDropdown.value != 0) { 
            string fileName = maoDireitaFileDropdown.options[maoDireitaFileDropdown.value].text + ".json";
            string filePath = getHandFilePath(maoDireitaDropdown).ToString() + "\\" + fileName;
            HandConfiguration configuration = controller.loadHandConfiguration(filePath); 
            controller.loadHandConfiguration(configuration, true);
        }
        else {
            controller.avatarSetupScript.bodyController.rightArmController.handController.reset();
        }
    }

    public void loadLeftHandConfiguration() {
        if (maoEsquerdaFileDropdown.value != 0) {
            string fileName = maoEsquerdaFileDropdown.options[maoEsquerdaFileDropdown.value].text + ".json";
            string filePath = getHandFilePath(maoEsquerdaDropdown).ToString() + "\\" + fileName;
            HandConfiguration configuration = controller.loadHandConfiguration(filePath);
            controller.loadHandConfiguration(configuration, false);
        }
        else {
            controller.avatarSetupScript.bodyController.leftArmController.handController.reset();
        }
    }

}
