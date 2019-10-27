using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger {
    public Transform fingerNail, distalPhalange, intermediatePhalange, proximalPhalange, initial;
    public GameObject fingerNailTarget;
    public float radius = 0.25f;
    public Finger(Transform proximalPhalange) {
        this.proximalPhalange = proximalPhalange;
        this.intermediatePhalange = this.proximalPhalange.GetChild(0);
        this.distalPhalange = this.intermediatePhalange.GetChild(0);
        this.fingerNail = this.distalPhalange.GetChild(0);
        this.initial = fingerNail;
    }

    public void setIkTargets(Transform wrist) {
        RootMotion.FinalIK.IKSolver.Bone[] bones = new RootMotion.FinalIK.IKSolver.Bone[4];
        bones[0] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[0].transform = proximalPhalange;
        bones[1] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[1].transform = intermediatePhalange;
        bones[2] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[2].transform = distalPhalange;
        bones[3] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[3].transform = fingerNail;
  
        fingerNailTarget = new GameObject();
        fingerNailTarget.name = proximalPhalange.name + "target";
        fingerNailTarget.transform.position = fingerNail.transform.position;
        fingerNailTarget.transform.rotation = fingerNail.transform.rotation;
        fingerNailTarget.transform.localScale = fingerNail.transform.localScale;
        fingerNailTarget.transform.SetParent(wrist);

        RootMotion.FinalIK.FABRIK script = proximalPhalange.gameObject.AddComponent<RootMotion.FinalIK.FABRIK>();
        script.solver.target = fingerNailTarget.transform;
        script.solver.bones = bones;
        script.enabled = true;
    }
    public void createGizmo() {
        GameObject sphereGizmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereGizmo.transform.SetParent(fingerNail);
        sphereGizmo.transform.localPosition = new Vector3(0,0,0);
        sphereGizmo.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        sphereGizmo.GetComponent<SphereCollider>().enabled = false;
    }
    public void createColliders() {
        SphereCollider collider = fingerNail.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
    }
    public void reset() {
        fingerNail = initial;
        fingerNailTarget.transform.position = initial.position;
        fingerNailTarget.transform.rotation = initial.rotation;
        fingerNailTarget.transform.localScale = initial.localScale;
    }
    public void setMouseDrag() {
        MouseDragTargeting mouseDrag = fingerNail.gameObject.AddComponent<MouseDragTargeting>();
        mouseDrag.target = fingerNailTarget;
    }

}
