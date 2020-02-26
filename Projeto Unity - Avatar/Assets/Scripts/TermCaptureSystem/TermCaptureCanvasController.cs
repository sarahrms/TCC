using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TermCaptureCanvasController : MonoBehaviour {
    public Vector3 frontCameraPosition, frontCameraRotation, topCameraPosition, topCameraRotation, rightCameraPosition, rightCameraRotation,
        leftCameraPosition, leftCameraRotation;
    public Camera frontCamera, topCamera, rightCamera, leftCamera;
    public Toggle rightHandToggle, leftHandToggle;
    public bool overwriteRightHand = false, overwriteLeftHand =  false;
    public bool isWord = false;

    public TermCaptureSystemController controller;
    public GROUP selectedGroup;
    public Dropdown cameraDropdown, maoDireitaDropdown, maoDireitaFileDropdown, 
        maoEsquerdaDropdown, maoEsquerdaFileDropdown, rostoDropdown, rostoFileDropdown, 
        bodyDropdown, bodyFileDropdown, maoDireitaMovimentoFileDropdown, maoDireitaMovimentoDropdown,
        maoEsquerdaMovimentoFileDropdown, maoEsquerdaMovimentoDropdown, headMovementFileDropdown,
        maoDireitaFingerMovementFileDropdown, maoEsquerdaFingerMovementFileDropdown,
        canvasTypeDropdown;
    public InputField nomeTermoSimples, nomeTermoComposto;
    public GameObject rightHandInterface, leftHandInterface;
    public GameObject simpleCanvas, compositeCanvas;

    void Start() {
        getInitialCameraPositions();
        setCameras();
        if (isWord) {

        }
        else { 
            changeFileOptionsMaoEsquerda();
            changeFileOptionsMaoDireita();
            changeFileOptionsRosto();
            changeFileOptionsBody();
            changeFileOptionsMovementRightHand();
            changeFileOptionsMovementLeftHand();
            changeFileOptionsFingersMovement();
            changeFileOptionsHeadMovement();
        }
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
            disableRightFingersMovementConfigurationInterface();
        }
        else {
            controller.avatarSetupScript.bodyController.rightArmController.handController.resetFingers();
            enableRightFingersMovementConfigurationInterface();
        }
    }

    public void loadLeftHandConfiguration() {
        if (maoEsquerdaFileDropdown.value != 0) {
            string fileName = maoEsquerdaFileDropdown.options[maoEsquerdaFileDropdown.value].text + ".json";
            string filePath = getHandFilePath(maoEsquerdaDropdown).ToString() + "\\" + fileName;
            HandConfiguration configuration = controller.loadConfiguration<HandConfiguration>(filePath);
            controller.loadHandConfiguration(configuration, false);
            disableLeftFingersMovementConfigurationInterface();
        }
        else {
            controller.avatarSetupScript.bodyController.leftArmController.handController.resetFingers();
            enableLeftFingersMovementConfigurationInterface();
        }
    }

    public void loadFaceConfiguration() {
       if (rostoFileDropdown.value != 0) {
            FaceConfiguration configuration = controller.loadConfiguration<FaceConfiguration>(getFaceConfigurationPath());
            controller.loadFaceConfiguration(configuration);
        }
        else {
            controller.avatarSetupScript.bodyController.headController.resetAnimation();
        }
    }

    public void loadBodyConfiguration() {
        if (bodyFileDropdown.value != 0) {
            BodyConfiguration configuration = controller.loadConfiguration<BodyConfiguration>(getBodyConfigurationPath());
            controller.loadBodyConfiguration(configuration);
        }
        else {
            controller.avatarSetupScript.bodyController.reset();
        }
    }

    public void loadRightHandMovementConfiguration() {
        if (maoDireitaMovimentoFileDropdown.value != 0) {
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(getRightHandMovementConfigurationPath());
            controller.loadMovementConfiguration(configuration, true, overwriteRightHand);           
        }
        else {
            controller.avatarSetupScript.bodyController.rightArmController.handController.resetWrist();
        }
    }

    public void loadLeftHandMovementConfiguration() {
        if (maoEsquerdaMovimentoFileDropdown.value != 0) {           
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(getLeftHandMovementConfigurationPath());
            controller.loadMovementConfiguration(configuration, false, overwriteLeftHand);
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
            controller.loadMovementConfiguration(configuration, true, false);
            disableRightHandConfigurationInterface();
        }
        else {
            loadRightHandConfiguration();
            enableRightHandConfigurationInterface();
        }
    }

    public void loadLeftHandFingerMovementConfiguration() {
        if (maoEsquerdaFingerMovementFileDropdown.value != 0) {
            string fileName = maoEsquerdaFingerMovementFileDropdown.options[maoEsquerdaFingerMovementFileDropdown.value].text + ".json";
            string filePath = getFingerMovementFilePath().ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, false, false);
            disableLeftHandConfigurationInterface();
        }
        else {
            loadLeftHandConfiguration();
            enableLeftHandConfigurationInterface();
        }
    }

    public void loadHeadMovementConfiguration() {
        if (headMovementFileDropdown.value != 0) {
            string fileName = headMovementFileDropdown.options[headMovementFileDropdown.value].text + ".json";
            string filePath = getHeadMovementFilePath().ToString() + "\\" + fileName;
            MovementConfiguration configuration = controller.loadConfiguration<MovementConfiguration>(filePath);
            controller.loadMovementConfiguration(configuration, false, false);
        }
        else {
            controller.avatarSetupScript.bodyController.headController.reset();
        }
    }

    public void enableRightHandConfigurationInterface() {
        rightHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = true;
        rightHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = true;
    }

    public void enableLeftHandConfigurationInterface() {
        leftHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = true;
        leftHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = true;
    }

    public void disableRightHandConfigurationInterface() {
        rightHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = false;
        rightHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = false;
    }

    public void disableLeftHandConfigurationInterface() {
        leftHandInterface.transform.GetChild(0).GetComponent<Dropdown>().interactable = false;
        leftHandInterface.transform.GetChild(1).GetComponent<Dropdown>().interactable = false;
    }

    public void enableRightFingersMovementConfigurationInterface() {
        maoDireitaFingerMovementFileDropdown.interactable = true; 
    }

    public void enableLeftFingersMovementConfigurationInterface() {
        maoEsquerdaFingerMovementFileDropdown.interactable = true;
    }

    public void disableRightFingersMovementConfigurationInterface() {
        maoDireitaFingerMovementFileDropdown.interactable = false;
    }

    public void disableLeftFingersMovementConfigurationInterface() {
        maoEsquerdaFingerMovementFileDropdown.interactable = false;
    }

    public void setToggleMaoEsquerda() {
        overwriteLeftHand = leftHandToggle.isOn;
    }

    public void setToggleMaoDireita() {
        overwriteRightHand = rightHandToggle.isOn;
    }

    public void salvarTermo() {
        Term termo = new Term();
        termo.name = nomeTermoSimples.text;

        BodyData bodyData = new BodyData();
        bodyData.faceConfigurationPath = getFaceConfigurationPath();
        bodyData.bodyConfigurationPath = getBodyConfigurationPath();
        bodyData.headMovementConfigurationPath = getHeadMovementConfigurationPath();

        ArmData rightArmData = new ArmData();
        Transform rightHand = GameObject.Find("mixamorig:RightHand - Target").transform;
        rightArmData.handMovementConfigurationPath = getRightHandMovementConfigurationPath();
        rightArmData.handPosition = rightHand.position;
        rightArmData.handRotation = rightHand.rotation.eulerAngles;
        rightArmData.overwriteHand = overwriteRightHand;

        HandData rightHandData = new HandData();
        rightHandData.handConfigurationPath = getRightHandConfigurationPath();
        rightHandData.fingersMovementConfigurationPath = getRightHandFingersMovementConfigurationPath();

        ArmData leftArmData = new ArmData();
        Transform leftHand = GameObject.Find("mixamorig:LeftHand - Target").transform;
        leftArmData.handMovementConfigurationPath = getLeftHandMovementConfigurationPath();
        leftArmData.handPosition = leftHand.position;
        leftArmData.handRotation = leftHand.rotation.eulerAngles;
        leftArmData.overwriteHand = overwriteLeftHand;

        HandData leftHandData = new HandData();
        leftHandData.handConfigurationPath = getLeftHandConfigurationPath();
        leftHandData.fingersMovementConfigurationPath = getLeftHandFingersMovementConfigurationPath();

        rightArmData.handData = rightHandData;
        leftArmData.handData = leftHandData;

        bodyData.rightArm = rightArmData;
        bodyData.leftArm = leftArmData;

        termo.bodyData = bodyData;
        termo.save();
}

    public string getFaceConfigurationPath() {
        return getRostoFilePath(rostoDropdown).ToString() + "\\" + rostoFileDropdown.options[rostoFileDropdown.value].text + ".json";
    }

    public string getBodyConfigurationPath() {
        return getBodyFilePath(bodyDropdown).ToString() + "\\" + bodyFileDropdown.options[bodyFileDropdown.value].text + ".json"; ;
    }

    public string getHeadMovementConfigurationPath() {
        return getHeadMovementFilePath().ToString() + "\\" + headMovementFileDropdown.options[headMovementFileDropdown.value].text + ".json";
    }

    public string getRightHandConfigurationPath() { 
        return getHandFilePath(maoDireitaDropdown).ToString() + "\\" + maoDireitaFileDropdown.options[maoDireitaFileDropdown.value].text + ".json";
    }

    public string getLeftHandConfigurationPath() { 
        return getHandFilePath(maoEsquerdaDropdown).ToString() + "\\" + maoEsquerdaFileDropdown.options[maoEsquerdaFileDropdown.value].text + ".json";
    }

    public string getRightHandMovementConfigurationPath() {
        return getMovementFilePath(maoDireitaMovimentoDropdown).ToString() + "\\" + maoDireitaMovimentoFileDropdown.options[maoDireitaMovimentoFileDropdown.value].text + ".json";
    }

    public string getLeftHandMovementConfigurationPath() {
        return getMovementFilePath(maoEsquerdaMovimentoDropdown).ToString() + "\\" + maoEsquerdaMovimentoFileDropdown.options[maoEsquerdaMovimentoFileDropdown.value].text + ".json";
    }

    public string getRightHandFingersMovementConfigurationPath() {
        return getFingerMovementFilePath().ToString() + "\\" + maoDireitaFingerMovementFileDropdown.options[maoDireitaFingerMovementFileDropdown.value].text + ".json";
    }

    public string getLeftHandFingersMovementConfigurationPath() {
        return getFingerMovementFilePath().ToString() + "\\" + maoEsquerdaFingerMovementFileDropdown.options[maoEsquerdaFingerMovementFileDropdown.value].text + ".json";
    }

    public void salvarPalavra() {
        Word palavra = new Word();
        palavra.name = nomeTermoComposto.text;
        palavra.termList = new List<string>();
        Transform termAggregator = compositeCanvas.transform.GetChild(3);
        for(int i=1; i<termAggregator.childCount; i++) { 
            Transform termo = termAggregator.GetChild(i).GetChild(0);
            palavra.termList.Add(termo.GetComponent<Dropdown>().options[termo.GetComponent<Dropdown>().value].text);
        }
        palavra.save();
    }
}
