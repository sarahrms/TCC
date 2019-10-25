using System.Collections.Generic;
using UnityEngine;

public class Hand {
    public Transform wrist;
    public GameObject wristTarget;
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

    public void createColliders() {
        SphereCollider collider = wrist.gameObject.AddComponent<SphereCollider>();
        collider.radius = 2.5f;

        foreach (Finger finger in fingers) {
            finger.createColliders();
        }
    }
}
