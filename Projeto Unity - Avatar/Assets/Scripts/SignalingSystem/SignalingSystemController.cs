using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SignalingSystemController : MonoBehaviour {
    public AvatarSignalingSetup avatarSetupScript;
    public HandComponent rightHand, leftHand;
    void Start() {
        avatarSetupScript = GameObject.Find("Avatar").GetComponent<AvatarSignalingSetup>();
        avatarSetupScript.init();
    }
    private void Update() {
        avatarSetupScript.update();
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

    public void loadHandConfiguration(HandConfiguration configuration, bool rightHand) {
        for (int i = 0; i < configuration.positions.Count; i++) {
           if (rightHand) {
                FingerController fingerController = avatarSetupScript.bodyController.rightArmController.handController.fingerControllers[i];
                Vector3 position = configuration.positions[i];
                fingerController.setTarget(position);
                Debug.Log(position);
            }
            else {
                FingerController fingerController = avatarSetupScript.bodyController.leftArmController.handController.fingerControllers[i];
                Vector3 position = new Vector3(-configuration.positions[i].x, configuration.positions[i].y, configuration.positions[i].z);
                fingerController.setTarget(position);
                Debug.Log(position);
            }
        }
    }

    public void loadWristConfiguration(WristConfiguration configuration, bool rightHand) {
        HandController handController;
        if (rightHand) { 
            handController = avatarSetupScript.bodyController.rightArmController.handController;
        }      
        else {
            handController = avatarSetupScript.bodyController.leftArmController.handController;
        }
        handController.setTarget(configuration.handPosition, configuration.handRotation);
    }

    public void loadFaceConfiguration(FaceConfiguration configuration) {
       /* for (int i = 0; i < configuration.positions.Count; i++) {
            if (rightHand) {
                FingerController fingerController = avatarSetupScript.bodyController.rightArmController.handController.fingerControllers[i];
                Vector3 position = configuration.positions[i];
                fingerController.setTarget(position);
                Debug.Log(position);
            }
            else {
                FingerController fingerController = avatarSetupScript.bodyController.leftArmController.handController.fingerControllers[i];
                Vector3 position = new Vector3(-configuration.positions[i].x, configuration.positions[i].y, configuration.positions[i].z);
                fingerController.setTarget(position);
                Debug.Log(position);
            }
        }*/
    }

    public void loadBodyConfiguration(BodyConfiguration configuration) {
        avatarSetupScript.bodyController.rightArmController.setTarget(configuration.rightShoulderPosition);
        avatarSetupScript.bodyController.leftArmController.setTarget(configuration.leftShoulderPosition);
        avatarSetupScript.bodyController.setTarget(configuration.spinePosition);
    }

    public void loadMovementConfiguration(MovementConfiguration configuration, bool rightHand) {
        configuration.loadConfigurationList();
        switch (configuration.type) {
            case MOVEMENT_TYPE.HANDS_MOVEMENT:                
                foreach (Configuration config in configuration.configurationList) {
                    WristConfiguration wristConfig = (WristConfiguration) config;
                    loadWristConfiguration(wristConfig, rightHand);
                    if (rightHand) {
                        while (!avatarSetupScript.bodyController.rightArmController.handController.isWristArrived()) {
                            //WaitForSeconds(0.1f);
                        }
                    }
                    else {
                        while (!avatarSetupScript.bodyController.leftArmController.handController.isWristArrived()) {
                            //WaitForSeconds(0.1f);
                        }
                    }
                }
                break;

            case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                Debug.Log(configuration.configurationList.ToString());
                foreach (Configuration config in configuration.configurationList) {
                    HandConfiguration handConfig = (HandConfiguration) config;

                    

                    loadHandConfiguration(handConfig, rightHand);


                    if (rightHand) {
                        while (!avatarSetupScript.bodyController.rightArmController.handController.isFingersArrived()) {
                            //WaitForSeconds(0.1f);
                        }
                    }
                    else {
                        while (!avatarSetupScript.bodyController.leftArmController.handController.isFingersArrived()) {
                            //WaitForSeconds(0.1f);
                        }
                    }
                }


                break;
            case MOVEMENT_TYPE.HEAD_MOVEMENT:
              


                break;

             /*   foreach (int i = 0; i < configuration.positions.Count; i++) {
            if (rightHand) {
                FingerController fingerController = avatarSetupScript.bodyController.rightArmController.handController.fingerControllers[i];
                Vector3 position = configuration.positions[i];
                fingerController.setTarget(position);
                Debug.Log(position);
            }
            else {
                FingerController fingerController = avatarSetupScript.bodyController.leftArmController.handController.fingerControllers[i];
                Vector3 position = new Vector3(-configuration.positions[i].x, configuration.positions[i].y, configuration.positions[i].z);
                fingerController.setTarget(position);
                Debug.Log(position);
            }*/
        }
    }

}
