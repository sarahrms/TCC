using UnityEngine;

public class Body {
    public Transform spine, initial;
    public GameObject spineTarget;
    public float radius = 3.0f;
    public Arm rightArm, leftArm;

    public Body(Transform spine) {
        this.spine = spine;
        this.initial = spine;
        leftArm = new Arm(GameObject.Find("mixamorig:LeftArm").transform);
        rightArm = new Arm(GameObject.Find("mixamorig:RightArm").transform);
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

        leftArm.setIkTargets(ikScript);
        rightArm.setIkTargets(ikScript);
    }

    public void createColliders() {
        SphereCollider collider = spineTarget.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
        leftArm.createColliders();
        rightArm.createColliders();
    }

    public void reset() {
        spine = initial;
        spineTarget.transform.position = initial.position;
        spineTarget.transform.rotation = initial.rotation;
        spineTarget.transform.localScale = initial.localScale;
        leftArm.reset();
        rightArm.reset();
    }
    public void createGizmo() {
    
    }
    public void setMouseDrag() {
        MouseDragTargeting mouseDrag = spine.gameObject.AddComponent<MouseDragTargeting>();
        mouseDrag.target = spineTarget.transform;

        leftArm.setMouseDrag();
        rightArm.setMouseDrag();
    }
}
