using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SignalingSystemController : MonoBehaviour { 
    public SignalingAvatarSetup avatarSetupScript;

    public List<Term> currentTermList;
    public Word currentWord;
    public Term currentTerm;
    public Animator animator;
    public int currentTermIndex = 0;

    public bool isPlaying = false, isPlayingHead = false, isPlayingRightHand = false, isPlayingLeftHand = false,
        isPlayingRightHandFingers, isPlayingLeftHandFingers;

    public MovementConfiguration currentHeadMovementConfiguration;
    public int currentHeadMovementConfigurationIndex = 0;

    public bool overwriteRightHand, overwriteLeftHand;
    public Vector3 rightHandPosition, leftHandPosition, rightHandRotation, leftHandRotation;
    public Vector3 rightHandPositionOffset, leftHandPositionOffset, rightHandRotationOffset, leftHandRotationOffset;
    public MovementConfiguration currentRightHandMovementConfiguration, currentLeftHandMovementConfiguration;
    public int currentRightHandMovementConfigurationIndex = 0, currentLeftHandMovementConfigurationIndex = 0;

    public MovementConfiguration currentRightHandFingersMovementConfiguration, currentLeftHandFingersMovementConfiguration;
    public int currentRightHandFingersMovementConfigurationIndex = 0, currentLeftHandFingersMovementConfigurationIndex = 0;

    public HandConfiguration currentRightHandConfiguration, currentLeftHandConfiguration;

    void Start() {
        GameObject avatar = GameObject.Find("Avatar");
        if (avatar != null) {
            avatarSetupScript = avatar.GetComponent<SignalingAvatarSetup>();
            avatarSetupScript.init();
            animator = avatar.GetComponent<Animator>();
        }
    }

    private void Update() {
        avatarSetupScript.update();
        updateConfiguration();
    }

    public void updateHeadMovementConfiguration() {
        if(isPlayingHead && avatarSetupScript.bodyController.headController.isArrived()) {
            if (++currentHeadMovementConfigurationIndex < currentHeadMovementConfiguration.configurationList.Count) {
                setHeadMovementConfiguration();
            }
            else {
                isPlayingHead = false;
            }
        }
    }

    public void updateRightHandMovementConfiguration() {
        if (isPlayingRightHand && avatarSetupScript.bodyController.rightArmController.handController.isWristArrived()) {
            if (++currentRightHandMovementConfigurationIndex < currentRightHandMovementConfiguration.configurationList.Count) {
                setRightHandMovementConfiguration();
            }
            else {
                isPlayingRightHand = false;
            }
        }
    }

    public void updateLeftHandMovementConfiguration() {
        if (isPlayingLeftHand && avatarSetupScript.bodyController.leftArmController.handController.isWristArrived()) {
            if (++currentLeftHandMovementConfigurationIndex < currentLeftHandMovementConfiguration.configurationList.Count) {
                setLeftHandMovementConfiguration();
            }
            else {
                isPlayingLeftHand = false;
            }
        }
    }

    public void updateRightHandFingersMovementConfiguration() {
        if (isPlayingRightHandFingers && avatarSetupScript.bodyController.rightArmController.handController.isFingersArrived()) {
            if (++currentRightHandFingersMovementConfigurationIndex < currentRightHandFingersMovementConfiguration.configurationList.Count) {
                setRightHandFingersMovementConfiguration();
            }
            else {
                isPlayingRightHandFingers = false;
            }
        }
    }

    public void updateLeftHandFingersMovementConfiguration() {
        if (isPlayingLeftHandFingers && avatarSetupScript.bodyController.leftArmController.handController.isFingersArrived()) {
            if (++currentLeftHandFingersMovementConfigurationIndex < currentLeftHandFingersMovementConfiguration.configurationList.Count) {
                setLeftHandFingersMovementConfiguration();
            }
            else {
                isPlayingLeftHandFingers = false;
            }
        }
    }

    public void updateConfiguration() {
        if (isPlaying) {
            updateHeadMovementConfiguration();
            updateRightHandMovementConfiguration();
            updateLeftHandMovementConfiguration();
            updateRightHandFingersMovementConfiguration();
            updateLeftHandFingersMovementConfiguration();
            isDone();
        }
        else {
            nextTerm();
        }
    }

    public void nextTerm() {
        if (++currentTermIndex < currentTermList.Count) {
            currentTerm = currentTermList[currentTermIndex];
            loadBodyData(currentTerm.bodyData);
            isPlaying = true;
        }        
    }

    public void isDone() {
        isPlaying = isPlayingHead || isPlayingRightHand || isPlayingLeftHand || isPlayingRightHandFingers || isPlayingLeftHandFingers;
    }
   
    public void setRightHandFingersMovementConfiguration() {
        HandConfiguration handConfiguration = (HandConfiguration) currentRightHandFingersMovementConfiguration.configurationList[currentRightHandFingersMovementConfigurationIndex];
        loadHandConfiguration(handConfiguration, true);
    }

    public void setLeftHandFingersMovementConfiguration() {
        HandConfiguration handConfiguration = (HandConfiguration) currentLeftHandFingersMovementConfiguration.configurationList[currentLeftHandFingersMovementConfigurationIndex];
        loadHandConfiguration(handConfiguration, false);
    }

    public void loadHandConfiguration(HandConfiguration configuration, bool right) {
        for (int i = 0; i < configuration.positions.Count; i++) {
            if (right) {
                FingerController fingerController = avatarSetupScript.bodyController.rightArmController.handController.fingerControllers[i];
                Vector3 position = configuration.positions[i];
                fingerController.setTarget(position);
            }
            else {
                FingerController fingerController = avatarSetupScript.bodyController.leftArmController.handController.fingerControllers[i];
                Vector3 position = new Vector3(-configuration.positions[i].x, configuration.positions[i].y, configuration.positions[i].z);
                fingerController.setTarget(position);
            }
        }
    }

    public void setRightHandConfiguration() {
        loadHandConfiguration(currentRightHandConfiguration, true);
    }

    public void setLeftHandConfiguration() {
        loadHandConfiguration(currentLeftHandConfiguration, false);
    }

    public void loadRightHandData(HandData handData) {
        currentRightHandFingersMovementConfiguration = loadConfiguration<MovementConfiguration>(handData.fingersMovementConfigurationPath);
        if (currentRightHandFingersMovementConfiguration != null) {
            currentRightHandFingersMovementConfiguration.loadConfigurationList();
            currentRightHandFingersMovementConfigurationIndex = 0;
            setRightHandFingersMovementConfiguration();
        }
        else {
            currentRightHandConfiguration = loadConfiguration<HandConfiguration>(handData.handConfigurationPath);
            setRightHandConfiguration();
        }
    }

    public void loadLeftHandData(HandData handData) {
        currentLeftHandFingersMovementConfiguration = loadConfiguration<MovementConfiguration>(handData.fingersMovementConfigurationPath);
        if (currentLeftHandFingersMovementConfiguration != null) {
            currentLeftHandFingersMovementConfiguration.loadConfigurationList();
            currentLeftHandFingersMovementConfigurationIndex = 0;
            setLeftHandFingersMovementConfiguration();
        }
        else {
            currentLeftHandConfiguration = loadConfiguration<HandConfiguration>(handData.handConfigurationPath);
            setLeftHandConfiguration();
        }
    }

    public void setWristTrajectory(MovementConfiguration configuration, bool isRightHand) {
        HandController handController;
        if (isRightHand) {
            handController = avatarSetupScript.bodyController.rightArmController.handController;
            handController.trajectoryPlane = configuration.trajectoryPlane;
            handController.trajectoryDirection = (currentRightHandMovementConfigurationIndex == 0) ? currentRightHandMovementConfiguration.trajectoryDirections[currentRightHandMovementConfigurationIndex]
                                                     : currentRightHandMovementConfiguration.trajectoryDirections[currentRightHandMovementConfigurationIndex - 1];
            handController.trajectoryType = (currentRightHandMovementConfigurationIndex == 0) ? TRAJECTORY_TYPE.STRAIGHT : currentRightHandMovementConfiguration.trajectoryType;
        }
        else {
            handController = avatarSetupScript.bodyController.leftArmController.handController;
            handController.trajectoryPlane = configuration.trajectoryPlane;
            handController.trajectoryDirection = (currentLeftHandMovementConfigurationIndex == 0) ? currentLeftHandMovementConfiguration.trajectoryDirections[currentLeftHandMovementConfigurationIndex]
                                                     : currentLeftHandMovementConfiguration.trajectoryDirections[currentLeftHandMovementConfigurationIndex - 1];
            handController.trajectoryType = (currentLeftHandMovementConfigurationIndex == 0) ? TRAJECTORY_TYPE.STRAIGHT : currentLeftHandMovementConfiguration.trajectoryType;
        }
    }

    public void loadWristConfiguration(WristConfiguration configuration, bool isRightHand) {
        HandController handController;
        if (isRightHand) {
            handController = avatarSetupScript.bodyController.rightArmController.handController;
            if (overwriteRightHand) {
                if (currentRightHandMovementConfigurationIndex == 0) {
                    rightHandPositionOffset = rightHandPosition - configuration.handPosition;
                    rightHandRotationOffset = rightHandRotation - configuration.handRotation;
                }
                handController.setTarget(configuration.handPosition + rightHandPositionOffset, configuration.handRotation + rightHandRotationOffset);                
            }
            else {
                handController.setTarget(configuration.handPosition, configuration.handRotation);
            }
        }
        else {
            handController = avatarSetupScript.bodyController.leftArmController.handController;
            Vector3 position = new Vector3(-configuration.handPosition.x, configuration.handPosition.y, configuration.handPosition.z);
            Vector3 rotation = new Vector3(configuration.handRotation.x, -configuration.handRotation.y, -configuration.handRotation.z);
            if (overwriteLeftHand) {
                if (currentLeftHandMovementConfigurationIndex == 0) {
                    leftHandPositionOffset = leftHandPosition - position;
                    leftHandRotationOffset = leftHandRotation - rotation;
                }
                handController.setTarget(position + leftHandPositionOffset, rotation + leftHandRotationOffset);
            }
            else {
                handController.setTarget(position, rotation);
            }
        }
    }

    public void setRightHandMovementConfiguration() {
        WristConfiguration wristConfiguration = (WristConfiguration) currentRightHandMovementConfiguration.configurationList[currentRightHandMovementConfigurationIndex];
        loadWristConfiguration(wristConfiguration, true);
        setWristTrajectory(currentRightHandMovementConfiguration, true);
    }

    public void setLeftHandMovementConfiguration() {
        WristConfiguration wristConfiguration = (WristConfiguration) currentLeftHandMovementConfiguration.configurationList[currentLeftHandMovementConfigurationIndex];
        loadWristConfiguration(wristConfiguration, false);
        setWristTrajectory(currentRightHandMovementConfiguration, false);
    }

    public void loadRightHandMovementConfiguration(ArmData armData) {
        currentRightHandMovementConfiguration = loadConfiguration<MovementConfiguration>(armData.handMovementConfigurationPath);
        if (currentRightHandMovementConfiguration != null) {
            currentRightHandMovementConfiguration.loadConfigurationList();
            currentRightHandMovementConfigurationIndex = 0;
            isPlayingRightHand = true;
            setRightHandMovementConfiguration();            
        }
        else {
            HandController handController = avatarSetupScript.bodyController.rightArmController.handController;
            handController.setTarget(rightHandPosition, rightHandRotation);
        }
    }

    public void loadLeftHandMovementConfiguration(ArmData armData) {
        currentLeftHandMovementConfiguration = loadConfiguration<MovementConfiguration>(armData.handMovementConfigurationPath);
        if (currentLeftHandMovementConfiguration != null) {
            currentLeftHandMovementConfiguration.loadConfigurationList();
            currentLeftHandMovementConfigurationIndex = 0;
            isPlayingLeftHand = true;
            setLeftHandMovementConfiguration();
        }
        else {
            HandController handController = avatarSetupScript.bodyController.leftArmController.handController;
            handController.setTarget(leftHandPosition, leftHandRotation);
        }
    }

    public void loadRightArmData(ArmData armData) {
        overwriteRightHand = armData.overwriteHand;
        rightHandPosition = armData.handPosition;
        rightHandRotation = armData.handRotation;
        loadRightHandMovementConfiguration(armData);

        loadRightHandData(armData.handData);
    }

    public void loadLeftArmData(ArmData armData) {
        overwriteLeftHand = armData.overwriteHand;
        leftHandPosition = armData.handPosition;
        leftHandRotation = armData.handRotation;
        loadLeftHandMovementConfiguration(armData);

        loadLeftHandData(armData.handData);
    }

    public void loadHeadConfiguration(HeadConfiguration configuration) {
        avatarSetupScript.bodyController.headController.setTarget(configuration.headPosition, configuration.headRotation);
    }

    public void setHeadMovementConfiguration() {
        HeadConfiguration headConfiguration = (HeadConfiguration) currentHeadMovementConfiguration.configurationList[currentHeadMovementConfigurationIndex];
        loadHeadConfiguration(headConfiguration);
    }

    public void loadHeadMovementConfiguration(BodyData bodyData) {
        currentHeadMovementConfiguration = loadConfiguration<MovementConfiguration>(bodyData.headMovementConfigurationPath);
        if(currentHeadMovementConfiguration != null){
            currentHeadMovementConfiguration.loadConfigurationList();
            currentHeadMovementConfigurationIndex = 0;
            isPlayingHead = true;
            setHeadMovementConfiguration();
        }
    }

    public void setFaceConfiguration(FaceConfiguration faceConfiguration) {
        AnimationClip clip = Resources.Load<AnimationClip>("Animations/" + faceConfiguration.animation);

        if (clip == null) {
            Debug.Log("Animation " + faceConfiguration.animation + " not found.");
        }

        //aqui é onde o override é feito
        AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animatorOverrideController["Empty"] = clip;
        animator.runtimeAnimatorController = animatorOverrideController;

        animator.SetBool("sinalizacaoFacial", true);        
    }

    public void loadFaceConfiguration(BodyData bodyData) {
        FaceConfiguration faceConfiguration = loadConfiguration<FaceConfiguration>(bodyData.faceConfigurationPath);
        if(faceConfiguration!=null){ setFaceConfiguration(faceConfiguration); }
    }

    public void setBodyConfiguration(BodyConfiguration configuration) {
        avatarSetupScript.bodyController.rightArmController.setTarget(configuration.rightShoulderPosition);
        avatarSetupScript.bodyController.leftArmController.setTarget(configuration.leftShoulderPosition);
        avatarSetupScript.bodyController.setTarget(configuration.spinePosition);
    }

    public void loadBodyConfiguration(BodyData bodyData) {
        BodyConfiguration bodyConfiguration = loadConfiguration<BodyConfiguration>(bodyData.bodyConfigurationPath);
        if(bodyConfiguration!=null){ setBodyConfiguration(bodyConfiguration); }
    }

    public void loadBodyData(BodyData bodyData) {
        loadBodyConfiguration(bodyData);
        loadFaceConfiguration(bodyData);
        loadHeadMovementConfiguration(bodyData);

        loadRightArmData(bodyData.rightArm);
        loadLeftArmData(bodyData.leftArm);
    }

    public T loadConfiguration<T>(string path) {
        try { 
            using (StreamReader streamReader = File.OpenText(path)) {
                string jsonString = streamReader.ReadToEnd();
                Symbol symbol = JsonUtility.FromJson<Symbol>(jsonString);
                T configuration = JsonUtility.FromJson<T>(symbol.configuration);
                return configuration;
            }
        }
        catch(IOException e) {
            Debug.Log(e);
            return default(T);
        }
    }

    public Term loadTerm(string name) {
        string path = "Assets\\Terms\\" + name + ".json";
        using (StreamReader streamReader = File.OpenText(path)) {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<Term>(jsonString);
        }      
    }   

    public void loadWord(string name) {
        string path = "Assets\\Words\\" + name + ".json";
        using (StreamReader streamReader = File.OpenText(path)) {
            string jsonString = streamReader.ReadToEnd();
            currentWord = JsonUtility.FromJson<Word>(jsonString);
        }
        currentTermList = new List<Term>();
        foreach(string termName in currentWord.termList) {
            Term term = loadTerm(termName);
            currentTermList.Add(term);
        }
    }
   
    public void setSignalling() {
        isPlaying = true;
        currentTermIndex = 0;
        currentTerm = currentTermList[currentTermIndex];
        loadBodyData(currentTerm.bodyData);
    }

    public void setSpeed(float speed) {
        avatarSetupScript.setSpeed(speed);
    }

}
