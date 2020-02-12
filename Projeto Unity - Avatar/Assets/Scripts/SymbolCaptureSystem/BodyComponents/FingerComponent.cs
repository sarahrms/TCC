using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerComponent : BasicBodyComponent {
    public Transform fingerNail, distalPhalange, intermediatePhalange, proximalPhalange, fingerNailTarget;
    public Vector3 initialFingerNailPosition;
    public float radius = 0.25f;

    public FingerComponent(Transform proximalPhalangeTransform) {
        proximalPhalange = proximalPhalangeTransform;
        intermediatePhalange = proximalPhalange.GetChild(0);
        distalPhalange = intermediatePhalange.GetChild(0);
        fingerNail = distalPhalange.GetChild(0);
        setLine();
    }

    public void reset() {
        fingerNailTarget.position = initialFingerNailPosition;
    }

    public void setIkTargets(Transform wrist) {
        RootMotion.FinalIK.IKSolver.Bone[] bones = new RootMotion.FinalIK.IKSolver.Bone[5];
        bones[0] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[0].transform = wrist;
        bones[1] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[1].transform = proximalPhalange;
        bones[2] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[2].transform = intermediatePhalange;
        bones[3] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[3].transform = distalPhalange;
        bones[4] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[4].transform = fingerNail;

        fingerNailTarget = new GameObject().transform;
        fingerNailTarget.name = proximalPhalange.name + " - Target";
        fingerNailTarget.SetParent(wrist);
        fingerNailTarget.position = fingerNail.position;
        fingerNailTarget.rotation = fingerNail.rotation;
        fingerNailTarget.localScale = fingerNail.localScale;

        initialFingerNailPosition = fingerNail.position;

        RootMotion.FinalIK.FABRIK script = proximalPhalange.gameObject.AddComponent<RootMotion.FinalIK.FABRIK>();
        script.solver.target = fingerNailTarget;
        script.solver.IKPositionWeight = 1;
        script.solver.bones = bones;
        script.enabled = true;

        addLimits();
    }

    public void addLimits() {
       // RootMotion.FinalIK.RotationLimit limits = distalPhalange.gameObject.AddComponent<RootMotion.FinalIK.RotationLimit>();
        // intermediatePhalange;
        //  proximalPhalange;
    }

    public void createGizmo() {
        createGizmo(fingerNailTarget, radius);
    }

    public void createCollider() {
        SphereCollider collider = fingerNailTarget.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
    }

    public void setMouseDrag() {     
        setMouseDrag(fingerNailTarget.gameObject);
    }

    public void update() {
        drawLine(fingerNail.position, fingerNailTarget.position);
    }

}
