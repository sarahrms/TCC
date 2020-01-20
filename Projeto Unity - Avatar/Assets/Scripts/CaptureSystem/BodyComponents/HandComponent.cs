using System.Collections.Generic;
using UnityEngine;

public class HandComponent : BasicBodyComponent {
    public Transform wrist, wristTarget;
    public Vector3 initialWristPosition, initialWristRotation;
    public float radius = 1.0f;
    public List<FingerComponent> fingers;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public HandComponent(Transform wristTransform) {
        wrist = wristTransform;
        initialWristPosition = wrist.position;
        initialWristRotation = wrist.rotation.eulerAngles;
        fingers = new List<FingerComponent>();
        for (int i = 0; i < 5; i++) {
            FingerComponent finger = new FingerComponent(wrist.GetChild(i));
            fingers.Add(finger);
        }
        setLine();
    }

    public void update() {
        if (wrist.name == "mixamorig:RightHand") {
            ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, wristTarget.position, 1);
            ikScript.solver.rightHandEffector.rotation = wristTarget.rotation;
            drawLine(ikScript.solver.rightHandEffector.position, wrist.position);
        }
        else if (wrist.name == "mixamorig:LeftHand") {
            ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, wristTarget.position, 1);
            ikScript.solver.leftHandEffector.rotation = wristTarget.rotation;
            drawLine(ikScript.solver.leftHandEffector.position, wrist.position);
        }

        foreach (FingerComponent finger in fingers) {
            finger.drawLine();
        }
    }

    public void reset() {
        wristTarget.position = initialWristPosition;
        wristTarget.rotation = Quaternion.Euler(initialWristRotation);

        foreach (FingerComponent finger in fingers) {
            finger.reset();
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

        foreach (FingerComponent finger in fingers) {
            finger.setIkTargets(wrist);
        }
    }

    public void createGizmo() {
        createGizmo(wristTarget, radius, Color.blue); 
        foreach (FingerComponent finger in fingers) {
            finger.createGizmo();
        }
    }

    public void createCollider() {
        createCollider(radius, wristTarget.gameObject);
        foreach (FingerComponent finger in fingers) {
            finger.createCollider();
        }
    }

    public void setMouseDrag() {
        setMouseDrag(wristTarget.gameObject);
        foreach (FingerComponent finger in fingers) {
            finger.setMouseDrag();
        }
    }
    
}
