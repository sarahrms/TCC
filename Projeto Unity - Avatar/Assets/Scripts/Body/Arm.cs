﻿using UnityEngine;
public class Arm {
    public Transform shoulder, initial;
    public GameObject shoulderTarget;
    public float radius = 1.5f;
    public Hand hand;

    public Arm(Transform shoulder) {
        this.shoulder = shoulder;
        this.initial = shoulder;
        this.hand = new Hand(shoulder.GetChild(0).GetChild(0).transform);
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        shoulderTarget = new GameObject();
        shoulderTarget.name = shoulder.gameObject.name + " - target";
        shoulderTarget.transform.position = shoulder.position;
        shoulderTarget.transform.rotation = shoulder.rotation;
        shoulderTarget.transform.localScale = shoulder.localScale;
        shoulderTarget.transform.SetParent(shoulder.parent);

        if (shoulder.gameObject.name == "mixamorig:RightArm") { 
            ikScript.solver.rightShoulderEffector.target = shoulderTarget.transform;
            ikScript.solver.rightShoulderEffector.positionWeight = 1;
        }
        else if(shoulder.gameObject.name == "mixamorig:LeftArm") { 
            ikScript.solver.leftShoulderEffector.target = shoulderTarget.transform;
            ikScript.solver.leftShoulderEffector.positionWeight = 1;
        }
        hand.setIkTargets(ikScript);        
    }
    public void createGizmo() {
        GameObject sphereGizmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereGizmo.transform.SetParent(shoulder);
        sphereGizmo.transform.localPosition = new Vector3(0, 0, 0);
        sphereGizmo.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        sphereGizmo.GetComponent<SphereCollider>().enabled = false;

        hand.createGizmo();
    }
    public void createColliders() {
        SphereCollider collider = shoulderTarget.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
        hand.createColliders();
    }
    public void setMouseDrag() {
        MouseDragTargeting mouseDrag = shoulderTarget.gameObject.AddComponent<MouseDragTargeting>();
        mouseDrag.target = shoulder.gameObject;
        hand.setMouseDrag();
    }
    public void reset() {
        shoulder = initial;
        shoulderTarget.transform.position = initial.position;
        shoulderTarget.transform.rotation = initial.rotation;
        shoulderTarget.transform.localScale = initial.localScale;
        hand.reset();
    }
}