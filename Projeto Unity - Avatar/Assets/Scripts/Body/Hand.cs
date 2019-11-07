using System.Collections.Generic;
using UnityEngine;

public class Hand {
    public Transform wrist, initial;
    public GameObject wristTarget;
    public float radius = 1.0f;
    public List<Finger> fingers;
    public Hand(Transform wrist) {
        this.wrist = wrist;
        
        fingers = new List<Finger>();
        for(int i=0; i < 5; i++) {
            Finger finger = new Finger(wrist.GetChild(i));
            fingers.Add(finger);
        }
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        wristTarget = new GameObject();
        wristTarget.name = wrist.gameObject.name + " - target";
        wristTarget.transform.position = wrist.position;
        wristTarget.transform.rotation = wrist.rotation;
        wristTarget.transform.localScale = wrist.localScale;
        wristTarget.transform.SetParent(wrist.parent);

        if (wrist.gameObject.name == "mixamorig:RightHand") {
            ikScript.solver.rightHandEffector.target = wristTarget.transform;
            ikScript.solver.rightHandEffector.positionWeight = 1;
        }
        else if (wrist.gameObject.name == "mixamorig:LeftHand") {
            ikScript.solver.leftHandEffector.target = wristTarget.transform;
            ikScript.solver.leftHandEffector.positionWeight = 1;
        }

        foreach (Finger finger in fingers) {
            finger.setIkTargets(wrist);
        }
    }
    public void createGizmo() {

    }
    public void createColliders() {
        SphereCollider collider = wristTarget.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;

        foreach (Finger finger in fingers) {
            finger.createColliders();
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
    public void setMouseDrag() {
        MouseDragTargeting mouseDrag = wrist.gameObject.AddComponent<MouseDragTargeting>();
        mouseDrag.target = wristTarget.transform;

        foreach (Finger finger in fingers) {
            finger.setMouseDrag();
        }
    }
}
