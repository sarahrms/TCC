using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : BasicBodyController {
    public Vector3 fingerNailInitialPosition, fingerNailTargetPosition;
    public Transform fingerNail, distalPhalange, intermediatePhalange, proximalPhalange,
        fingerNailTarget;
    public float speed = 1, constant = 0.2f;

    public FingerController(Transform proximalPhalangeTransform) {
        proximalPhalange = proximalPhalangeTransform;
        intermediatePhalange = proximalPhalange.GetChild(0);
        distalPhalange = intermediatePhalange.GetChild(0);
        fingerNail = distalPhalange.GetChild(0);
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
        fingerNailTarget.transform.SetParent(wrist);
        fingerNailTarget.transform.rotation = fingerNail.transform.rotation;
        fingerNailTarget.transform.localScale = fingerNail.transform.localScale;
        fingerNailTarget.transform.position = fingerNail.transform.position;

        fingerNailTargetPosition = fingerNailTarget.localPosition;
        fingerNailInitialPosition = fingerNailTarget.localPosition;

        RootMotion.FinalIK.FABRIK script = proximalPhalange.gameObject.AddComponent<RootMotion.FinalIK.FABRIK>();
        script.solver.target = fingerNailTarget.transform;
        script.solver.IKPositionWeight = 1;
        script.solver.bones = bones;
        script.enabled = true;

        addLimits();
    }

    public void reset() {
        fingerNailTargetPosition = fingerNailInitialPosition;
    }

    public void setTarget(Vector3 targetPosition) {
        fingerNailTargetPosition = targetPosition;
    }


    public void addLimits() {
        //RootMotion.FinalIK.RotationLimit limits = distalPhalange.gameObject.AddComponent<RootMotion.FinalIK.RotationLimit>();
       // intermediatePhalange;
      //  proximalPhalange;
    }
    public void update() {
        fingerNailTarget.transform.localPosition = Vector3.Lerp(fingerNailTarget.transform.localPosition, fingerNailTargetPosition, constant * speed);
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public bool isArrived() {
        Vector3 distance = fingerNailTarget.transform.localPosition - fingerNailTargetPosition;
        if(distance.sqrMagnitude < 0.2f) {
            return true;
        }
        return false;
    }
}
