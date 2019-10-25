using UnityEngine;

public class Body {
    public Transform spine;
    public GameObject spineTarget;
    public Arm rightArm, leftArm;

    public Body(Transform spine) {
        this.spine = spine;
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

    public void setMouseDrag() {

    }

    public void createColliders() {
        SphereCollider collider = spine.gameObject.AddComponent<SphereCollider>();
        collider.radius = 2.5f;
        leftArm.createColliders();
        rightArm.createColliders();
    }
}
