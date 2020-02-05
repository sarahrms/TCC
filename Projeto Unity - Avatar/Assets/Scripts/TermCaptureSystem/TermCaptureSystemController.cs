using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TermCaptureSystemController : MonoBehaviour {
    public TermCaptureAvatarSetup avatarSetupScript;
    public HandComponent rightHand, leftHand;
    public MOVEMENT_TYPE currentConfigurationType;
    public List<Configuration> configurationList;
    public int currentConfigurationIndex;
    public bool isRightHand, isPlaying = false;
    void Start() {
        avatarSetupScript = GameObject.Find("Avatar").GetComponent<TermCaptureAvatarSetup>();
        avatarSetupScript.init();
    }
    private void Update() {
        avatarSetupScript.update();
        updateConfiguration();
    }

    public void updateConfiguration() {
        if (isPlaying) { 
            switch (currentConfigurationType) {
                case MOVEMENT_TYPE.HANDS_MOVEMENT:
                    if(isRightHand) {
                        if (avatarSetupScript.bodyController.rightArmController.handController.isWristArrived()) {
                            if(++currentConfigurationIndex < configurationList.Count) {
                                loadHandMovementConfiguration();
                            }
                            else {
                                isPlaying = false;
                            }
                        }
                    }
                    else {
                        if (avatarSetupScript.bodyController.leftArmController.handController.isWristArrived()) {
                            if (++currentConfigurationIndex < configurationList.Count) {
                                loadHandMovementConfiguration();
                            }
                            else {
                                isPlaying = false;
                            }
                        }
                    }                
                    break;
                case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                 
                    break;
                case MOVEMENT_TYPE.HEAD_MOVEMENT:
                  
                    break;
            }
        }
    }

    public void loadHandMovementConfiguration() {
        WristConfiguration wristConfiguration = (WristConfiguration) configurationList[currentConfigurationIndex];
        loadWristConfiguration(wristConfiguration, isRightHand);
    }

    public void loadFingersMovementConfiguration() {
        HandConfiguration handConfiguration = (HandConfiguration) configurationList[currentConfigurationIndex];
        loadHandConfiguration(handConfiguration, isRightHand);
    }

    public void loadHeadMovementConfiguration() {
        HeadConfiguration headConfiguration = (HeadConfiguration) configurationList[currentConfigurationIndex];
        loadHeadConfiguration(headConfiguration);
    }

    public T loadConfiguration<T>(string path) {
        using (StreamReader streamReader = File.OpenText(path)) {
            string jsonString = streamReader.ReadToEnd();
            Symbol symbol = JsonUtility.FromJson<Symbol>(jsonString);
            Debug.Log(jsonString);
            T configuration = JsonUtility.FromJson<T>(symbol.configuration);
            return configuration;
        }
    }

    public void loadHandConfiguration(HandConfiguration configuration, bool isRightHand) {
        for (int i = 0; i < configuration.positions.Count; i++) {
           if (isRightHand) {
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

    public void loadWristConfiguration(WristConfiguration configuration, bool isRightHand) {
        HandController handController;
        if (isRightHand) { 
            handController = avatarSetupScript.bodyController.rightArmController.handController;
        }      
        else {
            handController = avatarSetupScript.bodyController.leftArmController.handController;
        }
        handController.setTarget(configuration.handPosition, configuration.handRotation);
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

    public void loadMovementConfiguration(MovementConfiguration configuration, bool isRight) {
        configuration.loadConfigurationList();
        isRightHand = isRight;
        isPlaying = true;
        currentConfigurationType = configuration.type;
        configurationList = configuration.configurationList;
        currentConfigurationIndex = 0;
        switch (currentConfigurationType) {
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

}
