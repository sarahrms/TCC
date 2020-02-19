using UnityEngine;

public class HeadController : BasicBodyController {
    public Transform spineRoot, spine, neck, head, headTarget;
    public Vector3 headTargetPosition, initialNeckPosition, initialHeadPosition;
    public Quaternion headTargetRotation, initialNeckRotation, initialHeadRotation;
    public HandController handController;
    public RootMotion.FinalIK.LimbIK ikScript;
    public float speed = 1, constant = 0.2f;

    public HeadController(Transform headTransform) {
        head = headTransform;
        neck = head.parent;
        spine = neck.parent;
        spineRoot = spine.parent;

        initialNeckPosition = neck.position;
        initialNeckRotation = neck.rotation;
        initialHeadPosition = head.position;
        initialHeadRotation = head.rotation;
    }

    public void setIkTargets(Transform neck) {
        headTarget = new GameObject().transform;
        headTarget.name = head.name + " - Target";
        headTarget.position = head.position;
        headTarget.rotation = head.rotation;
        headTarget.localScale = head.localScale;
        headTarget.SetParent(neck);

        headTargetPosition = headTarget.position;
        headTargetRotation = headTarget.rotation;

        ikScript = spineRoot.gameObject.AddComponent<RootMotion.FinalIK.LimbIK>();
        ikScript.solver.target = headTarget;
        ikScript.solver.IKPositionWeight = 1;
        ikScript.solver.IKRotationWeight = 1;
        ikScript.solver.SetChain(spineRoot, spine, neck, head);
        ikScript.enabled = true;
    }

    public void resetAnimation() { }

    public void reset() {
        headTargetPosition = initialHeadPosition;
        headTargetRotation = initialHeadRotation;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    public void setTarget(Vector3 targetPosition, Vector3 targetRotation) {
        headTargetPosition = targetPosition;
        headTargetRotation = Quaternion.Euler(targetRotation);
    }

    public void update() {
        headTarget.transform.position = Vector3.Lerp(headTarget.transform.position, headTargetPosition, constant * speed);
        headTarget.transform.rotation = Quaternion.Lerp(headTarget.transform.rotation, headTargetRotation, constant * speed);
    }

    public bool isArrived() {
        Vector3 distancePosition, distanceRotation;
        distancePosition = headTarget.transform.position - headTargetPosition;
        distanceRotation = headTarget.transform.rotation.eulerAngles - headTargetRotation.eulerAngles;
        return (distancePosition.sqrMagnitude < 1 && distanceRotation.sqrMagnitude < 1);
    }

}
