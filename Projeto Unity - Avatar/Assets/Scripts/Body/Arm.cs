using UnityEngine;

public class Arm : BasicBodyComponent {
    public Transform shoulder, initial;
    public GameObject shoulderTarget;
    public float radius = 1.5f;
    public Hand hand;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public void createGizmo() {
        //createGizmo(shoulder, radius, Color.green);
        createGizmo(shoulderTarget.transform, radius, Color.blue);
        hand.createGizmo();
    }

    public void createCollider() {
        createCollider(radius, shoulderTarget);
        hand.createCollider();
    }

    public void setMouseDrag() {
        setMouseDrag(shoulder, shoulderTarget);
        hand.setMouseDrag();
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        shoulderTarget = new GameObject();
        shoulderTarget.name = shoulder.gameObject.name + " - target";
        shoulderTarget.transform.position = shoulder.position;
        shoulderTarget.transform.rotation = shoulder.rotation;
        shoulderTarget.transform.localScale = shoulder.localScale;
        //shoulderTarget.transform.SetParent(shoulder.parent);

        if (shoulder.gameObject.name == "mixamorig:RightArm") {
            //ikScript.solver.rightShoulderEffector.target = shoulderTarget;
            ikScript.solver.rightShoulderEffector.positionWeight = 1;
            ikScript.solver.rightShoulderEffector.maintainRelativePositionWeight = 1;
        }
        else if (shoulder.gameObject.name == "mixamorig:LeftArm") {
            //ikScript.solver.leftShoulderEffector.target = shoulderTarget;
            ikScript.solver.leftShoulderEffector.positionWeight = 1;
            ikScript.solver.leftShoulderEffector.maintainRelativePositionWeight = 1;
        }
        hand.setIkTargets(ikScript);
    }

    public void update() {
        if (shoulder.gameObject.name == "mixamorig:RightArm") {
            ikScript.solver.rightShoulderEffector.position = Vector3.Lerp(ikScript.solver.rightShoulderEffector.position, shoulderTarget.transform.position, 1);
            drawLine(ikScript.solver.rightShoulderEffector.position, shoulder.transform.position);
        }
        else if (shoulder.gameObject.name == "mixamorig:LeftArm") {
            ikScript.solver.leftShoulderEffector.position = Vector3.Lerp(ikScript.solver.leftShoulderEffector.position, shoulderTarget.transform.position, 1);
            drawLine(ikScript.solver.leftShoulderEffector.position, shoulder.transform.position);
        }
        hand.update();
    }

    public Arm(Transform shoulder) {
        setLine();
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
