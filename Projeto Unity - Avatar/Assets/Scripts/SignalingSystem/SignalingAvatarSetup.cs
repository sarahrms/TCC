using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalingAvatarSetup : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public BodyController bodyController;

    public void init() {
        ikScript = gameObject.GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        bodyController = new BodyController(GameObject.Find("mixamorig:Spine").transform);
        bodyController.setIkTargets(ikScript, false);
    }

    public void update() {
        bodyController.update();
    }

    public void setSpeed(float speed) {
        bodyController.setSpeed(speed);
    }

    public void reset() {
        bodyController.reset();
    }
}
