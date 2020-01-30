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
        maoEsquerdaDropdown, maoEsquerdaFileDropdown, rostoDropdown, rostoFileDropdown, 
        bodyDropdown, bodyFileDropdown, movementDropdown, movementFileDropdown;

    void Start() {
        getInitialPositions();
        setCameras();
        changeFileOptionsMaoEsquerda();
        changeFileOptionsMaoDireita();
        changeFileOptionsRosto();
        changeFileOptionsBody();
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

    public void changeFileOptions(Dropdown dropdown, Dropdown fileDropdown, DirectoryInfo levelDirectoryPath) {
        FileInfo[] fileInfo;        
        fileInfo = levelDirectoryPath.GetFiles("*.json*", SearchOption.AllDirectories);
        fileDropdown.ClearOptions();
        fileDropdown.options.Add(new Dropdown.OptionData("Nenhuma"));
        foreach (FileInfo file in fileInfo) {
            if (!file.Extension.Contains("meta")) {
                fileDropdown.options.Add(new Dropdown.OptionData(file.Name.Remove(file.Name.IndexOf(file.Extension))));
            }
        }
        fileDropdown.RefreshShownValue();
    }

    public void changeFileOptionsMaoDireita() {
        DirectoryInfo levelDirectoryPath = getHandFilePath(maoDireitaDropdown);
        changeFileOptions(maoDireitaDropdown, maoDireitaFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsMaoEsquerda() {
        DirectoryInfo levelDirectoryPath = getHandFilePath(maoEsquerdaDropdown);
        changeFileOptions(maoEsquerdaDropdown, maoEsquerdaFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsRosto() {
        DirectoryInfo levelDirectoryPath = getRostoFilePath(rostoDropdown);
        changeFileOptions(rostoDropdown, rostoFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsBody() {
        DirectoryInfo levelDirectoryPath = getRostoFilePath(bodyDropdown);
        changeFileOptions(bodyDropdown, bodyFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsMovement() {
        DirectoryInfo levelDirectoryPath = getMovementFilePath(movementDropdown);
        changeFileOptions(movementDropdown, movementFileDropdown, levelDirectoryPath);
    }

    public DirectoryInfo getHandFilePath(Dropdown dropdown) {
        DirectoryInfo levelDirectoryPath;
        switch (dropdown.value) {
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
            case 4:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Five_Fingers");
                break;
            case 5:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Baby_Finger");
                break;
            case 6:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Ring_Finger");
                break;
            case 7:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Middle_Finger");
                break;
            case 8:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Index_Thumb");
                break;
            case 9:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Hand_Configuration\\Thumb");
                break;
            default:
                levelDirectoryPath = new DirectoryInfo("");
                break;
        }
        return levelDirectoryPath;
    }

    public DirectoryInfo getRostoFilePath(Dropdown dropdown) {
        DirectoryInfo levelDirectoryPath;
        switch (dropdown.value) {
            case 0:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Face_Configuration\\Brows_Eyes_EyeGaze");
                break;
            case 1:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Face_Configuration\\Cheek_Ears_Nose_Breath");
                break;
            case 2:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Face_Configuration\\Mouth_Lips");
                break;
            case 3:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Face_Configuration\\Tongue_Teeth_Chin_Neck");
                break;
            default:
                levelDirectoryPath = new DirectoryInfo("");
                break;
        }
        return levelDirectoryPath;
    }

    public DirectoryInfo getBodyFilePath(Dropdown dropdown) {
        DirectoryInfo levelDirectoryPath;
        switch (dropdown.value) {
            case 0:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Body_Configuration\\Shoulders Hips Torso");
                break;
            case 1:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Body_Configuration\\Limbs");
                break;
            default:
                levelDirectoryPath = new DirectoryInfo("");
                break;
        }
        return levelDirectoryPath;
    }

    public DirectoryInfo getMovementFilePath(Dropdown dropdown) {
        DirectoryInfo levelDirectoryPath;
        switch (dropdown.value) {
            case 0:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Brows_Eyes_EyeGaze");
                break;
            case 1:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Cheek_Ears_Nose_Breath");
                break;
            case 2:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Mouth_Lips");
                break;
            case 3:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Tongue_Teeth_Chin_Neck");
                break;
            default:
                levelDirectoryPath = new DirectoryInfo("");
                break;
        }
        return levelDirectoryPath;
    }

    public void loadConfiguration<T>() {

    }

    public void loadRightHandConfiguration() {
        if(maoDireitaFileDropdown.value != 0) { 
            string fileName = maoDireitaFileDropdown.options[maoDireitaFileDropdown.value].text + ".json";
            string filePath = getHandFilePath(maoDireitaDropdown).ToString() + "\\" + fileName;
            HandConfiguration configuration = controller.loadConfiguration<HandConfiguration>(filePath); 
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
            HandConfiguration configuration = controller.loadConfiguration<HandConfiguration>(filePath);
            controller.loadHandConfiguration(configuration, false);
        }
        else {
            controller.avatarSetupScript.bodyController.leftArmController.handController.reset();
        }
    }

    public void loadFaceConfiguration() {
       if (rostoFileDropdown.value != 0) {
            string fileName = rostoFileDropdown.options[rostoFileDropdown.value].text + ".json";
            string filePath = getRostoFilePath(rostoDropdown).ToString() + "\\" + fileName;
            FaceConfiguration configuration = controller.loadConfiguration<FaceConfiguration>(filePath);
            controller.loadFaceConfiguration(configuration);
        }
        else {
            controller.avatarSetupScript.bodyController.headController.resetAnimation();
        }
    }

    public void loadBodyConfiguration() {
        if (bodyFileDropdown.value != 0) {
            string fileName = bodyFileDropdown.options[bodyFileDropdown.value].text + ".json";
            string filePath = getBodyFilePath(bodyDropdown).ToString() + "\\" + fileName;
            BodyConfiguration configuration = controller.loadConfiguration<BodyConfiguration>(filePath);
            controller.loadBodyConfiguration(configuration);
        }
        else {
            controller.avatarSetupScript.bodyController.reset();
        }
    }

    public void loadMovementConfiguration() {
        if (movementFileDropdown.value != 0) {
            string fileName = movementFileDropdown.options[movementFileDropdown.value].text + ".json";
            string filePath = getBodyFilePath(movementDropdown).ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration);
        }
        else {
            controller.avatarSetupScript.bodyController.reset();
        }
    }
}
