using System.Collections.Generic;
using UnityEngine;

public class HandController : BasicBodyController {
    public Vector3 initialWristPosition, initialWristRotation;
    public Transform wrist, wristTarget;
    public List<FingerController> fingerControllers;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public float speed = 1;

    public HandController(Transform wrist) {
        this.wrist = wrist;
        initialWristPosition = wrist.position;
        initialWristRotation = wrist.rotation.eulerAngles;
        fingerControllers = new List<FingerController>();
        for (int i = 0; i < 5; i++) {
            FingerController fingerController = new FingerController(wrist.GetChild(i));
            fingerControllers.Add(fingerController);
        }
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        wristTarget = new GameObject().transform;
        wristTarget.name = wrist.gameObject.name + " - Target";
        wristTarget.position = wrist.position;
        wristTarget.rotation = wrist.rotation;
        wristTarget.localScale = wrist.localScale;

        if (wrist.name == "mixamorig:RightHand") {
            ikScript.solver.rightHandEffector.positionWeight = 1;
            ikScript.solver.rightHandEffector.rotationWeight = 1;
            ikScript.solver.rightHandEffector.maintainRelativePositionWeight = 1;
        }
        else if (wrist.name == "mixamorig:LeftHand") {
            ikScript.solver.leftHandEffector.positionWeight = 1;
            ikScript.solver.leftHandEffector.rotationWeight = 1;
            ikScript.solver.leftHandEffector.maintainRelativePositionWeight = 1;
        }

        for (int i = 0; i < 5; i++) {
            fingerControllers[i].setIkTargets(wrist);
        }
    }

    public void reset() {
        wristTarget.position = initialWristPosition;
        wristTarget.rotation = Quaternion.Euler(initialWristRotation);

        for (int i = 0; i < 5; i++) {
            fingerControllers[i].reset();
        }
    }

    public void setSpeed(float speed) {
        this.speed = speed;
        foreach (FingerController fingerController in fingerControllers) {
            fingerController.setSpeed(speed);
        }
    }

    public void setTarget(Vector3 targetPosition, Vector3 targetRotation) {
        wristTarget.position = targetPosition;
        wristTarget.rotation = Quaternion.Euler(targetRotation);
    }

    public void update() {
        if (wrist.name == "mixamorig:RightHand") {
            ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, wristTarget.position, 1);
            ikScript.solver.rightHandEffector.rotation = wristTarget.rotation;
        }
        else if (wrist.name == "mixamorig:LeftHand") {
            ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, wristTarget.position, 1);
            ikScript.solver.leftHandEffector.rotation = wristTarget.rotation;
        }

        foreach (FingerController fingerController in fingerControllers) {
            fingerController.update();
        }
    }

    public bool isArrived() {
        bool flag = true;
        foreach (FingerController fingerController in fingerControllers) {
           flag = fingerController.isArrived() ? flag : false;
        }
        return flag;
    }
 
}
