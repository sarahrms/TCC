﻿using System.Collections.Generic;
using UnityEngine;

public class HandController : BasicBodyController {
    public Vector3 initialWristPosition;
    public Quaternion initialWristRotation;
    public Transform wrist, wristTarget;
    public List<FingerController> fingerControllers;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public float speed = 1, radius = 1.0f;

    public HandController(Transform wrist) {
        this.wrist = wrist;
        initialWristPosition = wrist.position;
        initialWristRotation = wrist.rotation;
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

    public void resetFingers() {
        for (int i = 0; i < 5; i++) {
            fingerControllers[i].reset();
        }
    }

    public void resetWrist() {
        wristTarget.position = initialWristPosition;
        wristTarget.rotation = initialWristRotation;
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
            ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, wristTarget.position, 1 * speed);
            ikScript.solver.rightHandEffector.rotation = wristTarget.rotation;
        }
        else if (wrist.name == "mixamorig:LeftHand") {
            ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, wristTarget.position, 1 * speed);
            ikScript.solver.leftHandEffector.rotation = wristTarget.rotation;
        }

        foreach (FingerController fingerController in fingerControllers) {
            fingerController.update();
        }
    }
    public bool isWristArrived() {
        Vector3 distance = ikScript.solver.leftHandEffector.position - wristTarget.position;
        if (distance.sqrMagnitude < 0.2f) {
            return true;
        }
        return false;
    }

    public bool isFingersArrived() {
        bool flag = true;
        foreach (FingerController fingerController in fingerControllers) {
           flag = fingerController.isArrived() ? flag : false;
        }
        return flag;
    }

    public void createGizmo() {
        GameObject sphereGizmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereGizmo.transform.SetParent(wristTarget);
        sphereGizmo.transform.localPosition = new Vector3(0, 0, 0);
        sphereGizmo.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        sphereGizmo.GetComponent<SphereCollider>().enabled = false;
        sphereGizmo.GetComponent<MeshRenderer>().materials = new Material[] { Resources.Load("SphereMaterial") as Material };

    }

    public void createCollider() {
        SphereCollider collider = wristTarget.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
    }
    public void setMouseDrag() {
        wristTarget.gameObject.AddComponent<MouseDragTargeting>();
    }
}