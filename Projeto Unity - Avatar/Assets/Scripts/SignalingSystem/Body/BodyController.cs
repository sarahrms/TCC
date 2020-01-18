using UnityEngine;

public class BodyController : BasicBodyComponent {
 /*   public Transform spine, initial;
    public GameObject spineTarget;
    public float radius = 3.0f;
    public ArmComponent rightArm;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public void createGizmo() {
        createGizmo(spineTarget.transform, radius, Color.blue);
        rightArm.createGizmo();
    }

    public void createCollider() {
        createCollider(radius, spineTarget);
        rightArm.createCollider();
    }

    public void setMouseDrag() {
        setMouseDrag(spine, spineTarget);
        rightArm.setMouseDrag();
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        this.ikScript = ikScript;

        spineTarget = new GameObject();
        spineTarget.name = spine.gameObject.name + " - Target";
        spineTarget.transform.position = spine.position;
        spineTarget.transform.rotation = spine.rotation;
        spineTarget.transform.localScale = spine.localScale;

        ikScript.solver.bodyEffector.positionWeight = 1;
        ikScript.solver.bodyEffector.maintainRelativePositionWeight = 1;

        rightArm.setIkTargets(ikScript);
    }

    public void update() {
        ikScript.solver.bodyEffector.position = Vector3.Lerp(ikScript.solver.bodyEffector.position, spineTarget.transform.position, 1);
        drawLine(ikScript.solver.bodyEffector.position, spine.transform.position);

        rightArm.update();
    }

    public BodyController(Transform spineObject) {
        setLine();
        spine = spineObject;
        initial = spineObject; //OLHAR ISSO AQUI//
        rightArm = new ArmComponent(GameObject.Find("mixamorig:RightArm").transform);
    }
   
    public void reset() {
        reset(spineTarget.transform, initial);
        rightArm.reset();
    }
    */
}
