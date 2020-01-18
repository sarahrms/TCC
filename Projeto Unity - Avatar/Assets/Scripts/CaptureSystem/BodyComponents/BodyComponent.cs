using UnityEngine;

public class BodyComponent : BasicBodyComponent {
    public Transform spine, spineTarget;
    public Vector3 initialSpinePosition;
    public float radius = 3.0f;
    public ArmComponent rightArm;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public BodyComponent(Transform spineTransform) {
        spine = spineTransform;
        initialSpinePosition = spineTransform.position; 
        rightArm = new ArmComponent(GameObject.Find("mixamorig:RightArm").transform);
        setLine();
    }

    public void update() {
        ikScript.solver.bodyEffector.position = Vector3.Lerp(ikScript.solver.bodyEffector.position, spineTarget.position, 1);
        drawLine(ikScript.solver.bodyEffector.position, spine.position);
        rightArm.update();
    }

    public void reset() {
        spineTarget.position = initialSpinePosition;
        rightArm.reset();
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        spineTarget = new GameObject().transform;
        spineTarget.name = spine.gameObject.name + " - Target";
        spineTarget.position = spine.position;
        spineTarget.rotation = spine.rotation;
        spineTarget.localScale = spine.localScale;

        ikScript.solver.bodyEffector.positionWeight = 1;
        ikScript.solver.bodyEffector.maintainRelativePositionWeight = 1;

        rightArm.setIkTargets(ikScript);
    }

    public void createGizmo() {
        createGizmo(spineTarget, radius, Color.blue);
        rightArm.createGizmo();
    }

    public void createCollider() {
        createCollider(radius, spineTarget.gameObject);
        rightArm.createCollider();
    }

    public void setMouseDrag() {
        setMouseDrag(spineTarget.gameObject);
        rightArm.setMouseDrag();
    }
    
}
