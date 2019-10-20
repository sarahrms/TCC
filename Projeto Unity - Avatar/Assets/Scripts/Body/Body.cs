using UnityEngine;

public class Body {
    public Transform spine;
    public GameObject spineTarget;
    public Leg rightLeg, leftLeg;
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

        ikScript.solver.bodyEffector.target = spineTarget.transform;

        leftArm.setIkTargets(ikScript);
        rightArm.setIkTargets(ikScript);
    }

    public void createColliders() {
        SphereCollider collider = spine.gameObject.AddComponent<SphereCollider>() as SphereCollider;
        collider.radius = 4;
    }
}
