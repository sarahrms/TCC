using UnityEngine;

public class Body : BasicBodyComponent {
    public Transform spine, initial;
    public GameObject spineTarget;
    public float radius = 3.0f;
    public Arm rightArm, leftArm;

    public void createGizmo() {
        createGizmo(spine, radius, Color.green);
        createGizmo(spineTarget.transform, radius, Color.blue);
        leftArm.createGizmo();
        rightArm.createGizmo();
    }

    public void createColliders() {
        createCollider(radius, spineTarget);
        leftArm.createColliders();
        rightArm.createColliders();
    }

    public void setMouseDrag() {
        setMouseDrag(spine, spineTarget);
        leftArm.setMouseDrag();
        rightArm.setMouseDrag();
    }

    public void setIkTargets(RootMotion.FinalIK.FullBodyBipedIK ikScript) {
        spineTarget = new GameObject();
        spineTarget.name = spine.gameObject.name + " - target";
        spineTarget.transform.position = spine.position;
        spineTarget.transform.rotation = spine.rotation;
        spineTarget.transform.localScale = spine.localScale;
        spineTarget.transform.SetParent(spine.parent);

        ikScript.solver.bodyEffector.target = spineTarget.transform;
        ikScript.solver.bodyEffector.positionWeight = 1;
        ikScript.solver.bodyEffector.maintainRelativePositionWeight = 1;

        leftArm.setIkTargets(ikScript);
        rightArm.setIkTargets(ikScript);
    }

    public Body(Transform spineObject) {
        spine = spineObject;
        initial = spineObject; //OLHAR ISSO AQUI//
        leftArm = new Arm(GameObject.Find("mixamorig:LeftArm").transform);
        rightArm = new Arm(GameObject.Find("mixamorig:RightArm").transform);
    }
   



    public void reset() {
        reset(spineTarget.transform, initial);
        leftArm.reset();
        rightArm.reset();
    }
    
}
