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
        if(shoulder.name == "mixamorig:RightArm") { 
            ikScript.solver.rightShoulderEffector.position = Vector3.Lerp(ikScript.solver.rightShoulderEffector.position, shoulderTarget.position, 1);
            drawLine(ikScript.solver.rightShoulderEffector.position, shoulder.position);            
        }
        else if (shoulder.name == "mixamorig:LeftArm") {
            ikScript.solver.leftShoulderEffector.position = Vector3.Lerp(ikScript.solver.leftShoulderEffector.position, shoulderTarget.position, 1);
            drawLine(ikScript.solver.leftShoulderEffector.position, shoulder.position);            
        }
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

        if (shoulder.name == "mixamorig:RightArm") {
            ikScript.solver.rightShoulderEffector.positionWeight = 1;
            ikScript.solver.rightShoulderEffector.maintainRelativePositionWeight = 1;
        }
        else if (shoulder.name == "mixamorig:LeftArm") {
            ikScript.solver.leftShoulderEffector.positionWeight = 1;
            ikScript.solver.leftShoulderEffector.maintainRelativePositionWeight = 1;
        }
        hand.setIkTargets(ikScript);
    }

    public void createGizmo() {
        createGizmo(shoulderTarget, radius);
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
