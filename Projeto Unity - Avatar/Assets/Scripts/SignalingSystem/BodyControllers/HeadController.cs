using UnityEngine;

public class HeadController : BasicBodyController {
    public Vector3 initialHeadPosition, initialHeadRotation, headTargetPosition;
    public Transform head, headTarget;
    public HandController handController;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public float speed = 1;

    public HeadController(Transform headTransform) {
        head = headTransform;
        initialHeadPosition = headTransform.position;
        initialHeadRotation = headTransform.rotation.eulerAngles;
    }

    public void setIkTargets(Transform neck) {
        RootMotion.FinalIK.IKSolver.Bone[] bones = new RootMotion.FinalIK.IKSolver.Bone[2];

        bones[0] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[0].transform = neck;
        bones[1] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[1].transform = head;

        headTarget = new GameObject().transform;
        headTarget.name = head.name + " - Target";
        headTarget.position = head.position;
        headTarget.rotation = head.rotation;
        headTarget.localScale = head.localScale;
        headTarget.SetParent(neck);

        RootMotion.FinalIK.FABRIK script = neck.gameObject.AddComponent<RootMotion.FinalIK.FABRIK>();
        script.solver.target = headTarget;
        script.solver.IKPositionWeight = 1;
        script.solver.bones = bones;
        script.enabled = true;
    }

    public void reset() {
        headTarget.position = initialHeadPosition;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public void setTarget(Vector3 targetPosition) {
        headTargetPosition = targetPosition;
    }

    public void update() {
        headTarget.transform.localPosition = Vector3.Lerp(headTarget.transform.localPosition, headTargetPosition, 0.5f * speed);
    }

}
