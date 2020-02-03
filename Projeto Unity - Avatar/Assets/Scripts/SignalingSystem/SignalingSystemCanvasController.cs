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
        bodyDropdown, bodyFileDropdown, maoDireitaMovimentoFileDropdown, maoDireitaMovimentoDropdown,
        maoEsquerdaMovimentoFileDropdown, maoEsquerdaMovimentoDropdown, headMovementFileDropdown,
        maoDireitaFingerMovementFileDropdown, maoEsquerdaFingerMovementFileDropdown;

    public GameObject rightHandInterface, leftHandInterface;

    void Start() {
        getInitialPositions();
        setCameras();
        changeFileOptionsMaoEsquerda();
        changeFileOptionsMaoDireita();
        changeFileOptionsRosto();
        changeFileOptionsBody();
        changeFileOptionsMovementRightHand();
        changeFileOptionsMovementLeftHand();
        changeFileOptionsFingersMovement();
        changeFileOptionsHeadMovement();
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

    public void changeFileOptions(Dropdown fileDropdown, DirectoryInfo levelDirectoryPath) {
        FileInfo[] fileInfo;        
        fileInfo = levelDirectoryPath.GetFiles("*", SearchOption.AllDirectories);        
        fileDropdown.ClearOptions();
        fileDropdown.options.Add(new Dropdown.OptionData("Nenhuma"));
        foreach (FileInfo file in fileInfo) {
            if (!file.Extension.Contains("meta")) {
                fileDropdown.options.Add(new Dropdown.OptionData(file.Name.Remove(file.Name.IndexOf(file.Extension))));
            }
        }
        fileDropdown.RefreshShownValue();
    }

    public void changeFileOptionsHeadMovement() {
        DirectoryInfo levelDirectoryPath = getHeadMovementFilePath();
        changeFileOptions(headMovementFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsFingersMovement() {
        DirectoryInfo levelDirectoryPath = getFingerMovementFilePath();
        changeFileOptions(maoDireitaFingerMovementFileDropdown, levelDirectoryPath);
        changeFileOptions(maoEsquerdaFingerMovementFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsMaoDireita() {
        DirectoryInfo levelDirectoryPath = getHandFilePath(maoDireitaDropdown);
        changeFileOptions(maoDireitaFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsMaoEsquerda() {
        DirectoryInfo levelDirectoryPath = getHandFilePath(maoEsquerdaDropdown);
        changeFileOptions(maoEsquerdaFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsRosto() {
        DirectoryInfo levelDirectoryPath = getRostoFilePath(rostoDropdown);
        changeFileOptions(rostoFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsBody() {
        DirectoryInfo levelDirectoryPath = getBodyFilePath(bodyDropdown);
        changeFileOptions(bodyFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsMovementRightHand() {
        DirectoryInfo levelDirectoryPath = getMovementFilePath(maoDireitaMovimentoDropdown);
        changeFileOptions(maoDireitaMovimentoFileDropdown, levelDirectoryPath);
    }

    public void changeFileOptionsMovementLeftHand() {
        DirectoryInfo levelDirectoryPath = getMovementFilePath(maoEsquerdaMovimentoDropdown);
        changeFileOptions(maoEsquerdaMovimentoFileDropdown, levelDirectoryPath);
    }

    public DirectoryInfo getFingerMovementFilePath() {
        return new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Finger_Movement");
    }

    public DirectoryInfo getHeadMovementFilePath() {
        return new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Head");
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
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Body_Configuration\\Shoulders_Hips_Torso");
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
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Contact");
                break;
            case 1:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Straight_Wall_Plane");
                break;
            case 2:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Curves_Floor_Plane");
                break;
            case 3:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Straight_Diagonal_Plane");
                break;
            case 4:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Straight_Floor_Plane");
                break;
            case 5:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Curves_Wall_Plane");
                break;
            case 6:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Curves_Hit_Wall_Plane");
                break;
            case 7:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Curves_Hit_Floor_Plane");
                break;
            case 8:
                levelDirectoryPath = new DirectoryInfo("Assets\\Symbols\\Movement_Configuration\\Circle");
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
            HandConfiguration configuration = controller.loadConfiguration<HandConfiguration>(filePath); 
            controller.loadHandConfiguration(configuration, true);
        }
        else {
            controller.avatarSetupScript.bodyController.rightArmController.handController.resetFingers();
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
            controller.avatarSetupScript.bodyController.leftArmController.handController.resetFingers();
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

    public void loadRightHandMovementConfiguration() {
        if (maoDireitaMovimentoFileDropdown.value != 0) {
            string fileName = maoDireitaMovimentoFileDropdown.options[maoDireitaMovimentoFileDropdown.value].text + ".json";
            string filePath = getMovementFilePath(maoDireitaMovimentoDropdown).ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, true);           
        }
        else {
            controller.avatarSetupScript.bodyController.rightArmController.handController.resetWrist();
        }
    }

    public void loadLeftHandMovementConfiguration() {
        if (maoEsquerdaMovimentoFileDropdown.value != 0) {
            string fileName = maoEsquerdaMovimentoFileDropdown.options[maoEsquerdaMovimentoFileDropdown.value].text + ".json";
            string filePath = getMovementFilePath(maoEsquerdaMovimentoDropdown).ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, false);
        }
        else {
            controller.avatarSetupScript.bodyController.leftArmController.handController.resetWrist();
        }
    }

    public void loadRightHandFingerMovementConfiguration() {
        if (maoDireitaFingerMovementFileDropdown.value != 0) {
            string fileName = maoDireitaFingerMovementFileDropdown.options[maoDireitaFingerMovementFileDropdown.value].text + ".json";
            string filePath = getFingerMovementFilePath().ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, true);
            disableHandsConfigurationInterfaces();
        }
        else {
            loadRightHandConfiguration();
            enableHandsConfigurationInterface();
        }
    }

    public void loadLeftHandFingerMovementConfiguration() {
        if (maoEsquerdaMovimentoFileDropdown.value != 0) {
            string fileName = maoEsquerdaMovimentoFileDropdown.options[maoEsquerdaMovimentoFileDropdown.value].text + ".json";
            string filePath = getFingerMovementFilePath().ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, false);
            disableHandsConfigurationInterfaces();
        }
        else {
            loadLeftHandConfiguration();
            enableHandsConfigurationInterface();
        }
    }

    public void loadHeadMovementConfiguration() {
        if (headMovementFileDropdown.value != 0) {
            string fileName = headMovementFileDropdown.options[headMovementFileDropdown.value].text + ".json";
            string filePath = getHeadMovementFilePath().ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, false);
        }
        else {
            controller.avatarSetupScript.bodyController.headController.reset();
        }
    }

    public void enableHandsConfigurationInterface() {
        rightHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = true;
        rightHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = true;
        leftHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = true;
        leftHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = true;
    }

    public void disableHandsConfigurationInterfaces() {
        rightHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = false;
        rightHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = false;
        leftHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = false;
        leftHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = false;
    }
}
