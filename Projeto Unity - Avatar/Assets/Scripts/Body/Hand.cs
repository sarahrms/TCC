using System.Collections.Generic;
using UnityEngine;

public class Hand : BasicBodyComponent {
    public Transform wrist, initial;
    public GameObject wristTarget;
    public float radius = 1.0f;
    public List<Finger> fingers;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public void createGizmo() {
        //createGizmo(wrist, radius, Color.green);
        createGizmo(wristTarget.transform, radius, Color.blue); 
        foreach (Finger finger in fingers) {
            finger.createGizmo();
        }
    }

    public void createCollider() {
        createCollider(radius, wristTarget);
        foreach (Finger finger in fingers) {
            finger.createCollider();
        }
    }

    public void setMouseDrag() {
        setMouseDrag(wrist, wristTarget);
        foreach (Finger finger in fingers) {
            finger.setMouseDrag();
        }
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        wristTarget = new GameObject();
        wristTarget.name = wrist.gameObject.name + " - target";
        wristTarget.transform.position = wrist.position;
        wristTarget.transform.rotation = wrist.rotation;
        wristTarget.transform.localScale = wrist.localScale;
        //wristTarget.transform.SetParent(wrist.parent);

        if (wrist.gameObject.name == "mixamorig:RightHand") {
            //ikScript.solver.rightHandEffector.target = wristTarget.transform;
            ikScript.solver.rightHandEffector.positionWeight = 1;
            ikScript.solver.rightHandEffector.maintainRelativePositionWeight = 1;
        }
        else if (wrist.gameObject.name == "mixamorig:LeftHand") {
            //ikScript.solver.leftHandEffector.target = wristTarget.transform;
            ikScript.solver.leftHandEffector.positionWeight = 1;
            ikScript.solver.leftHandEffector.maintainRelativePositionWeight = 1;
        }

        foreach (Finger finger in fingers) {
            finger.setIkTargets(wrist);
        }
    }

    public void update() {
        if (wrist.gameObject.name == "mixamorig:RightHand") {
            ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, wristTarget.transform.position, 1);
            ikScript.solver.rightHandEffector.rotation = wristTarget.transform.rotation;
            drawLine(ikScript.solver.rightHandEffector.position, wrist.transform.position);
        }
        else if (wrist.gameObject.name == "mixamorig:LeftHand") {
            ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, wristTarget.transform.position, 1);
            ikScript.solver.leftHandEffector.rotation = wristTarget.transform.rotation;
            drawLine(ikScript.solver.leftHandEffector.position, wrist.transform.position);
        }
        foreach (Finger finger in fingers) {
            finger.drawLine();
        }
    }

    public Hand(Transform wrist) {
        setLine();
        this.wrist = wrist;
        fingers = new List<Finger>();
        for(int i=0; i < 5; i++) {
            Finger finger = new Finger(wrist.GetChild(i));
            fingers.Add(finger);
        }
    }

    public void reset() {
        wrist = initial;
        wristTarget.transform.position = initial.position;
        wristTarget.transform.rotation = initial.rotation;
        wristTarget.transform.localScale = initial.localScale;
        foreach (Finger finger in fingers) {
            finger.reset();
        }
    }

}
