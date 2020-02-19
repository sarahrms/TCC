using UnityEngine;

public class BodyController : BasicBodyController {
    public Transform spine, spineTarget;
    public Vector3 initialSpinePosition;
    public ArmController rightArmController, leftArmController;
    public HeadController headController;
    public float speed = 1, constant = 0.1f;

    RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public BodyController(Transform spineTransform) {
        initialSpinePosition = spineTransform.position;
        spine = spineTransform;
        rightArmController = new ArmController(GameObject.Find("mixamorig:RightArm").transform);
        leftArmController = new ArmController(GameObject.Find("mixamorig:LeftArm").transform);
        headController = new HeadController(GameObject.Find("mixamorig:Head").transform);
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript, bool createGizmo) {
        this.ikScript = ikScript;

        spineTarget = new GameObject().transform;
        spineTarget.name = spine.gameObject.name + " - Target";
        spineTarget.transform.position = spine.position;
        spineTarget.transform.rotation = spine.rotation;
        spineTarget.transform.localScale = spine.localScale;

        ikScript.solver.bodyEffector.positionWeight = 1;
        ikScript.solver.bodyEffector.maintainRelativePositionWeight = 1;

        rightArmController.setIkTargets(ikScript, createGizmo);
        leftArmController.setIkTargets(ikScript, createGizmo);
        headController.setIkTargets(GameObject.Find("mixamorig:Neck").transform);
    }

    public void reset() {
        spineTarget.position = initialSpinePosition;
        headController.reset();
        rightArmController.reset();
        leftArmController.reset();
    }

    public void setSpeed(float speed) { 
        this.speed = speed;
        headController.setSpeed(speed);
        rightArmController.setSpeed(speed);
        leftArmController.setSpeed(speed);
    }

    public void setTarget(Vector3 targetPosition) {
        spineTarget.position = targetPosition;
    }

    public void update() {
        ikScript.solver.bodyEffector.position = Vector3.Lerp(ikScript.solver.bodyEffector.position, spineTarget.transform.position, constant * speed);
        headController.update();
        rightArmController.update();
        leftArmController.update();
    }
}
