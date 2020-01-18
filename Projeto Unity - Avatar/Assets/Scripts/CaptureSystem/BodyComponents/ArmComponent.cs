using UnityEngine;

public class ArmComponent : BasicBodyComponent {
    public Transform shoulder, shoulderTarget;
    public Vector3 initialShoulderPosition;
    public float radius = 1.5f;
    public HandComponent hand;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public ArmComponent(Transform shoulderTransform) {
        shoulder = shoulderTransform;
        initialShoulderPosition = shoulderTransform.position;
        hand = new HandComponent(shoulder.GetChild(0).GetChild(0).transform);
        setLine();
    }

    public void update() {
        ikScript.solver.rightShoulderEffector.position = Vector3.Lerp(ikScript.solver.rightShoulderEffector.position, shoulderTarget.position, 1);
        drawLine(ikScript.solver.rightShoulderEffector.position, shoulder.position);
        hand.update();
    }

    public void reset() {
        shoulderTarget.position = initialShoulderPosition;
        hand.reset();
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        shoulderTarget = new GameObject().transform;
        shoulderTarget.name = shoulder.name + " - Target";
        shoulderTarget.position = shoulder.position;
        shoulderTarget.rotation = shoulder.rotation;
        shoulderTarget.localScale = shoulder.localScale;

        ikScript.solver.rightShoulderEffector.positionWeight = 1;
        ikScript.solver.rightShoulderEffector.maintainRelativePositionWeight = 1;

        hand.setIkTargets(ikScript);
    }

    public void createGizmo() {
        createGizmo(shoulderTarget, radius, Color.blue);
        hand.createGizmo();
    }

    public void createCollider() {
        createCollider(radius, shoulderTarget.gameObject);
        hand.createCollider();
    }

    public void setMouseDrag() {
        setMouseDrag(shoulderTarget.gameObject);
        hand.setMouseDrag();
    }

}
