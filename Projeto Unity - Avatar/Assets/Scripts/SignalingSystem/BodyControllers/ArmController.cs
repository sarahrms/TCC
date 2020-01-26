using UnityEngine;

public class ArmController : BasicBodyController {
    public Vector3 initialShoulderPosition;
    public Transform shoulder, shoulderTarget; 
    public HandController handController;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public float speed = 1;

    public ArmController(Transform shoulderTransform) {
        shoulder = shoulderTransform;
        initialShoulderPosition = shoulder.position;
        handController = new HandController(shoulder.GetChild(0).GetChild(0).transform);
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        shoulderTarget = new GameObject().transform;
        shoulderTarget.name = shoulder.gameObject.name + " - Target";
        shoulderTarget.transform.position = shoulder.position;
        shoulderTarget.transform.rotation = shoulder.rotation;
        shoulderTarget.transform.localScale = shoulder.localScale;

        ikScript.solver.rightShoulderEffector.positionWeight = 1;
        ikScript.solver.rightShoulderEffector.maintainRelativePositionWeight = 1;

        handController.setIkTargets(ikScript);
    }

    public void reset() {
        shoulderTarget.position = initialShoulderPosition;
        handController.reset();
    }

    public void setSpeed(float speed) {
        this.speed = speed;
        handController.setSpeed(speed);
    }

    public void setTarget(Vector3 targetPosition) {
        shoulderTarget.position = targetPosition;
    }

    public void update() {
        if (shoulder.name == "mixamorig:RightArm") {
            ikScript.solver.rightShoulderEffector.position = Vector3.Lerp(ikScript.solver.rightShoulderEffector.position, shoulderTarget.position, 1*speed);
        }
        else if (shoulder.name == "mixamorig:LeftArm") {
            ikScript.solver.leftShoulderEffector.position = Vector3.Lerp(ikScript.solver.leftShoulderEffector.position, shoulderTarget.position, 1*speed);
        }
        handController.update();
    }

}
