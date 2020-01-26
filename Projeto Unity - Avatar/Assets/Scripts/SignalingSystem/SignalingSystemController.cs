using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SignalingSystemController : MonoBehaviour {
    public AvatarSignalingSetup avatarSetupScript;
    void Start() {
        avatarSetupScript = GameObject.Find("Avatar").GetComponent<AvatarSignalingSetup>();
        avatarSetupScript.init();
    }
    private void Update() {
        avatarSetupScript.update();
    }

    public HandConfiguration loadHandConfiguration(string path) {
        using (StreamReader streamReader = File.OpenText(path)) {
            string jsonString = streamReader.ReadToEnd();
            Symbol symbol = JsonUtility.FromJson<Symbol>(jsonString);
            HandConfiguration configuration = JsonUtility.FromJson<HandConfiguration>(symbol.configuration);
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


}
