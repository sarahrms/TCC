using System.Collections.Generic;
using UnityEngine;

public enum TRAJECTORY_TYPE {
    CURVE,
    STRAIGHT
}

public class HandController : BasicBodyController {
    public TRAJECTORY_TYPE trajectoryType = TRAJECTORY_TYPE.STRAIGHT;
    public TRAJECTORY_PLANE trajectoryPlane = TRAJECTORY_PLANE.XY;
    public TRAJECTORY_DIRECTION trajectoryDirection = TRAJECTORY_DIRECTION.CLOCK_WISE;
    public Vector3 initialWristPosition, lastTargetPosition;
    public Quaternion initialWristRotation;
    public Transform wrist, wristTarget;
    public List<FingerController> fingerControllers;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public float speed = 1, radius = 1.0f, constant = 5f;

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
        lastTargetPosition = wristTarget.position;
        wristTarget.position = targetPosition;
        wristTarget.rotation = Quaternion.Euler(targetRotation);
    }

    public void update() {
        if (trajectoryType == TRAJECTORY_TYPE.STRAIGHT) {      
            if (wrist.name == "mixamorig:RightHand") {
                ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, wristTarget.position, constant * speed * Time.fixedDeltaTime);
                ikScript.solver.rightHandEffector.rotation = Quaternion.Lerp(ikScript.solver.rightHandEffector.rotation, wristTarget.rotation, constant * speed * Time.fixedDeltaTime);
            }
            else {
                ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, wristTarget.position, constant * speed * Time.fixedDeltaTime);
                ikScript.solver.leftHandEffector.rotation = Quaternion.Lerp(ikScript.solver.leftHandEffector.rotation, wristTarget.rotation, constant * speed * Time.fixedDeltaTime);
            }
        }
        else {
            if (wrist.name == "mixamorig:RightHand") {
                Vector3 centerPoint = lastTargetPosition + (wristTarget.position - lastTargetPosition) / 2;
                
                Transform centerObj = new GameObject().transform;
                centerObj.position = centerPoint;

                Transform rotatedObj = new GameObject().transform;
                rotatedObj.position = ikScript.solver.rightHandEffector.position;
                rotatedObj.SetParent(centerObj);

                int direction = trajectoryDirection.Equals(TRAJECTORY_DIRECTION.CLOCK_WISE) ? 1 : -1;

                Vector3 targetPosition = new Vector3();

                switch (trajectoryPlane) {
                    case TRAJECTORY_PLANE.XY: { 
                        centerObj.Rotate(new Vector3(0, 0, constant * speed * direction));
                        targetPosition = rotatedObj.position;
                        Vector3 zLerp = Vector3.Lerp(new Vector3(0, 0, targetPosition.z), new Vector3(0, 0, wristTarget.position.z), constant * speed * Time.fixedDeltaTime);
                        targetPosition.z = zLerp.z;
                        break;
                    }
                    case TRAJECTORY_PLANE.XZ:
                        centerObj.Rotate(new Vector3(0, constant * speed * direction, 0));
                        break;
                    case TRAJECTORY_PLANE.YZ:
                        centerObj.Rotate(new Vector3(constant * speed * direction, 0, 0));
                        break;
                }
                
                GameObject.Destroy(centerObj.gameObject);
                GameObject.Destroy(rotatedObj.gameObject);

                ikScript.solver.rightHandEffector.position = Vector3.Lerp(ikScript.solver.rightHandEffector.position, targetPosition, 1);
                ikScript.solver.rightHandEffector.rotation = Quaternion.Lerp(ikScript.solver.rightHandEffector.rotation, wristTarget.rotation, constant * speed * Time.fixedDeltaTime);
            }
            else {
                Vector3 centerPoint = lastTargetPosition + (wristTarget.position - lastTargetPosition) / 2;

                Transform centerObj = new GameObject().transform;
                centerObj.position = centerPoint;

                Transform rotatedObj = new GameObject().transform;
                rotatedObj.position = ikScript.solver.leftHandEffector.position;
                rotatedObj.SetParent(centerObj);

                int direction = trajectoryDirection.Equals(TRAJECTORY_DIRECTION.CLOCK_WISE) ? 1 : -1;

                Vector3 targetPosition = new Vector3();
                switch (trajectoryPlane) {
                    case TRAJECTORY_PLANE.XY: { 
                        centerObj.Rotate(new Vector3(0, 0, constant * speed * direction));
                        targetPosition = rotatedObj.position;
                        Vector3 zLerp = Vector3.Lerp(new Vector3(0, 0, targetPosition.z), new Vector3(0, 0, wristTarget.position.z), constant * speed * Time.fixedDeltaTime);
                        targetPosition.z = zLerp.z;
                        break;
                    }
                    case TRAJECTORY_PLANE.XZ:
                        centerObj.Rotate(new Vector3(0, constant * speed * direction, 0));
                        break;
                    case TRAJECTORY_PLANE.YZ:
                        centerObj.Rotate(new Vector3(contant * speed * direction, 0, 0));
                        break;
                }
                
                GameObject.Destroy(centerObj.gameObject);
                GameObject.Destroy(rotatedObj.gameObject);

                ikScript.solver.leftHandEffector.position = Vector3.Lerp(ikScript.solver.leftHandEffector.position, targetPosition, 1);
                ikScript.solver.leftHandEffector.rotation = Quaternion.Lerp(ikScript.solver.leftHandEffector.rotation, wristTarget.rotation, constant * speed * Time.fixedDeltaTime);
            }
        }
        foreach (FingerController fingerController in fingerControllers) {
            fingerController.update();
        }
    }

    public bool isWristArrived() {
        Vector3 distancePosition, distanceRotation;
        if (wrist.name == "mixamorig:RightHand") {
            distancePosition = ikScript.solver.rightHandEffector.position - wristTarget.position;
            distanceRotation = ikScript.solver.rightHandEffector.rotation.eulerAngles - wristTarget.rotation.eulerAngles;
        }
        else {
            distancePosition = ikScript.solver.leftHandEffector.position - wristTarget.position;
            distanceRotation = ikScript.solver.leftHandEffector.rotation.eulerAngles - wristTarget.rotation.eulerAngles;
        }
        return (distancePosition.sqrMagnitude < 1 && distanceRotation.sqrMagnitude < 1);
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
