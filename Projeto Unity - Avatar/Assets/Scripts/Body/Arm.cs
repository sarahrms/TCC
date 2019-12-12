using UnityEngine;
public class Arm : BasicBodyComponent{
    public Transform shoulder, initial;
    public GameObject shoulderTarget;
    public float radius = 1.5f;
    public Hand hand;

    public void createGizmo() {
        createGizmo(shoulder, radius, Color.green);
        createGizmo(shoulderTarget.transform, radius, Color.blue);
        hand.createGizmo();
    }

    public void createColliders() {
        createCollider(radius, shoulderTarget);
        hand.createColliders();
    }

    public void setMouseDrag() {
        setMouseDrag(shoulder, shoulderTarget);
        hand.setMouseDrag();
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        shoulderTarget = new GameObject();
        shoulderTarget.name = shoulder.gameObject.name + " - target";
        shoulderTarget.transform.position = shoulder.position;
        shoulderTarget.transform.rotation = shoulder.rotation;
        shoulderTarget.transform.localScale = shoulder.localScale;
        shoulderTarget.transform.SetParent(shoulder.parent);

        if (shoulder.gameObject.name == "mixamorig:RightArm") {
            ikScript.solver.rightShoulderEffector.target = shoulderTarget.transform;
            ikScript.solver.rightShoulderEffector.positionWeight = 1;
            ikScript.solver.rightShoulderEffector.maintainRelativePositionWeight = 1;
        }
        else if (shoulder.gameObject.name == "mixamorig:LeftArm") {
            ikScript.solver.leftShoulderEffector.target = shoulderTarget.transform;
            ikScript.solver.leftShoulderEffector.positionWeight = 1;
            ikScript.solver.leftShoulderEffector.maintainRelativePositionWeight = 1;
        }
        hand.setIkTargets(ikScript);
    }

    public Arm(Transform shoulder) {
        this.shoulder = shoulder;
        this.initial = shoulder;
        this.hand = new Hand(shoulder.GetChild(0).GetChild(0).transform);
    }

   


    public void reset() {
        shoulder = initial;
        shoulderTarget.transform.position = initial.position;
        shoulderTarget.transform.rotation = initial.rotation;
        shoulderTarget.transform.localScale = initial.localScale;
        hand.reset();
    }
}
