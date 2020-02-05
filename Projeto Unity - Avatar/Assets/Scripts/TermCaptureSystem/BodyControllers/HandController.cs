using System.Collections.Generic;
using UnityEngine;

public enum TRAJECTORY_TYPE {
    CURVE,
    CIRCLE,
    STRAIGHT
}

public class HandController : BasicBodyController {
    public TRAJECTORY_TYPE trajectoryType = TRAJECTORY_TYPE.STRAIGHT;
    public Vector3 initialWristPosition;
    public Quaternion initialWristRotation;
    public Transform wrist, wristTarget;
    public List<FingerController> fingerControllers;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public float speed = 1, radius = 1.0f, constant = 0.15f;

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
        else {
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
        switch (trajectoryType) {
            case TRAJECTORY_TYPE.STRAIGHT:        
                if (wrist.name == "mixamorig:RightHand") {
                    ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, wristTarget.position, constant * speed);
                    ikScript.solver.rightHandEffector.rotation = Quaternion.Lerp(ikScript.solver.rightHandEffector.rotation, wristTarget.rotation, constant * speed);
                }
                else {
                    ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, wristTarget.position, constant * speed);
                    ikScript.solver.leftHandEffector.rotation = Quaternion.Lerp(ikScript.solver.leftHandEffector.rotation, wristTarget.rotation, constant * speed);
                }
                break;
            case TRAJECTORY_TYPE.CIRCLE:

                break;
            case TRAJECTORY_TYPE.CURVE:

               break;
        }
        foreach (FingerController fingerController in fingerControllers) {
            fingerController.update();
        }
    }

    public bool isWristArrived() {
        Vector3 distance;
        if (wrist.name == "mixamorig:RightHand") {
            distance = ikScript.solver.rightHandEffector.position - wristTarget.position;
        }
        else {
            distance = ikScript.solver.leftHandEffector.position - wristTarget.position;
        }
        if (distance.sqrMagnitude < 1) {
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
