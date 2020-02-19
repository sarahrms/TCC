using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TermCaptureSystemController : MonoBehaviour {
    public TermCaptureAvatarSetup avatarSetupScript;
    public HandComponent rightHand, leftHand;
    public MovementConfiguration currentConfiguration;
    public int currentIndex;
    public bool isRightHand = false, isPlaying = false, overwriteHand = false;
    Vector3 positionOffset, rotationOffset;
    //Vector3 initialLeftHandTargetRotation = new Vector3(16.911f, 54.966f, 157.637f);
    //Vector3 initialRightHandTargetRotation = new Vector3(18.148f, -54.532f, -157.141f);

    void Start() {       
        if(GameObject.Find("Avatar") != null){
            avatarSetupScript = GameObject.Find("Avatar").GetComponent<TermCaptureAvatarSetup>();
            avatarSetupScript.init(); 
        }
    }

    private void Update() {
        if (avatarSetupScript != null) {
            avatarSetupScript.update();
            updateConfiguration();
        }
    }

    public void updateConfiguration() {
        if (isPlaying) { 
            switch (currentConfiguration.movementType) {
                case MOVEMENT_TYPE.HANDS_MOVEMENT:
                    if(isRightHand) {
                        if (avatarSetupScript.bodyController.rightArmController.handController.isWristArrived()) {
                            if(++currentIndex < currentConfiguration.configurationList.Count) {
                                loadHandMovementConfiguration();
                            }
                            else {
                                isPlaying = false;
                                positionOffset = new Vector3();
                                rotationOffset = new Vector3();
                                avatarSetupScript.bodyController.rightArmController.handController.trajectoryType = TRAJECTORY_TYPE.STRAIGHT;
                            }
                        }
                    }
                    else {
                        if (avatarSetupScript.bodyController.leftArmController.handController.isWristArrived()) {
                            if (++currentIndex < currentConfiguration.configurationList.Count) {
                                loadHandMovementConfiguration();
                            }
                            else {
                                isPlaying = false;
                                positionOffset = new Vector3();
                                rotationOffset = new Vector3();
                                avatarSetupScript.bodyController.leftArmController.handController.trajectoryType = TRAJECTORY_TYPE.STRAIGHT;
                            }
                        }
                    }                
                    break;
                case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                    if (isRightHand) {
                        if (avatarSetupScript.bodyController.rightArmController.handController.isFingersArrived()) {
                            if (++currentIndex < currentConfiguration.configurationList.Count) {
                                loadFingersMovementConfiguration();
                            }
                            else {
                                isPlaying = false;
                            }
                        }
                    }
                    else {
                        if (avatarSetupScript.bodyController.leftArmController.handController.isFingersArrived()) {
                            if (++currentIndex < currentConfiguration.configurationList.Count) {
                                loadFingersMovementConfiguration();
                            }
                            else {
                                isPlaying = false;
                            }
                        }
                    }
                    break;
                case MOVEMENT_TYPE.HEAD_MOVEMENT:
                    if (avatarSetupScript.bodyController.headController.isArrived()) {
                        if (++currentIndex < currentConfiguration.configurationList.Count) {
                            loadHeadMovementConfiguration();
                        }
                        else {
                            isPlaying = false;
                        }
                    }
                    break;
            }
        }
    }

    public void loadMovementConfiguration(MovementConfiguration configuration, bool isRight, bool overwrite) {
        configuration.loadConfigurationList();
        overwriteHand = overwrite;
        isRightHand = isRight;
        isPlaying = true;
        currentConfiguration = configuration;
        currentIndex = 0;
        switch (currentConfiguration.movementType) {
            case MOVEMENT_TYPE.HANDS_MOVEMENT:
                loadHandMovementConfiguration();
                break;
            case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                loadFingersMovementConfiguration();
                break;
            case MOVEMENT_TYPE.HEAD_MOVEMENT:
                loadHeadMovementConfiguration();
                break; 
        }
    }

    public void loadHandMovementConfiguration() {
        WristConfiguration wristConfiguration = (WristConfiguration) currentConfiguration.configurationList[currentIndex];
        loadWristConfiguration(wristConfiguration);
        setWristTrajectory();
    }

    public void setWristTrajectory() {
        HandController handController;
        if (isRightHand) {
            handController = avatarSetupScript.bodyController.rightArmController.handController;
        }
        else {
            handController = avatarSetupScript.bodyController.leftArmController.handController;
        }

        handController.trajectoryPlane = currentConfiguration.trajectoryPlane;
        handController.trajectoryDirection = (currentIndex==0) ? currentConfiguration.trajectoryDirections[currentIndex] 
                                                 : currentConfiguration.trajectoryDirections[currentIndex-1];
        handController.trajectoryType = (currentIndex == 0) ? TRAJECTORY_TYPE.STRAIGHT : currentConfiguration.trajectoryType;
    }

    public void loadFingersMovementConfiguration() {
        HandConfiguration handConfiguration = (HandConfiguration) currentConfiguration.configurationList[currentIndex];
        loadHandConfiguration(handConfiguration, isRightHand);
    }

    public void loadHeadMovementConfiguration() {
        HeadConfiguration headConfiguration = (HeadConfiguration) currentConfiguration.configurationList[currentIndex];
        loadHeadConfiguration(headConfiguration);
    }

    public T loadConfiguration<T>(string path) {
        using (StreamReader streamReader = File.OpenText(path)) {
            string jsonString = streamReader.ReadToEnd();
            Symbol symbol = JsonUtility.FromJson<Symbol>(jsonString);
            T configuration = JsonUtility.FromJson<T>(symbol.configuration);
            return configuration;
        }
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

    public void loadWristConfiguration(WristConfiguration configuration) {
        HandController handController;
        if (isRightHand) { 
            handController = avatarSetupScript.bodyController.rightArmController.handController;
            if (overwriteHand) {
                if (currentIndex == 0) {
                    Vector3 initialHandPosition = handController.ikScript.solver.rightHandEffector.position;
                    Vector3 initialHandRotation = handController.ikScript.solver.rightHandEffector.rotation.eulerAngles;
                    positionOffset = configuration.handPosition - initialHandPosition;
                    rotationOffset = configuration.handRotation - initialHandRotation;
                }                
                handController.setTarget(configuration.handPosition - positionOffset, configuration.handRotation - rotationOffset);
            }
            else {
                handController.setTarget(configuration.handPosition, configuration.handRotation);
            }
        }      
        else {
            handController = avatarSetupScript.bodyController.leftArmController.handController;
            Vector3 position = new Vector3(-configuration.handPosition.x, configuration.handPosition.y, configuration.handPosition.z);
            Vector3 rotation = new Vector3(configuration.handRotation.x, -configuration.handRotation.y, -configuration.handRotation.z);
            if (overwriteHand) {
                if (currentIndex == 0) {
                    Vector3 initialHandPosition = handController.ikScript.solver.leftHandEffector.position;
                    Vector3 initialHandRotation = handController.ikScript.solver.leftHandEffector.rotation.eulerAngles;
                    positionOffset = position - initialHandPosition;
                    rotationOffset = rotation - initialHandRotation;
                }
                handController.setTarget(position - positionOffset, rotation - rotationOffset);
            }
            else {
                handController.setTarget(position, rotation);
            }           
        }
    }

    public void loadFaceConfiguration(FaceConfiguration configuration) {
        //IMPLEMENTAR//
        ///////////////
        ///////////////
        ///////////////
        ///////////////
    }

    public void loadBodyConfiguration(BodyConfiguration configuration) {
        avatarSetupScript.bodyController.rightArmController.setTarget(configuration.rightShoulderPosition);
        avatarSetupScript.bodyController.leftArmController.setTarget(configuration.leftShoulderPosition);
        avatarSetupScript.bodyController.setTarget(configuration.spinePosition);
    }

    public void loadHeadConfiguration(HeadConfiguration configuration) {
        avatarSetupScript.bodyController.headController.setTarget(configuration.headPosition, configuration.headRotation);
    }

}
