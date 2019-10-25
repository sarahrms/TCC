using UnityEngine;
public class Arm {
    public Transform shoulder;
    public GameObject shoulderTarget;
    public Hand hand;

    public Arm(Transform shoulder) {
        this.shoulder = shoulder;
        this.hand = new Hand(shoulder.GetChild(0).GetChild(0).transform);
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
        }
        else if(shoulder.gameObject.name == "mixamorig:LeftArm") { 
            ikScript.solver.leftShoulderEffector.target = shoulderTarget.transform;
            ikScript.solver.leftShoulderEffector.positionWeight = 1;
        }
        hand.setIkTargets(ikScript);        
    }

    public void createColliders() {
        SphereCollider collider = shoulder.gameObject.AddComponent<SphereCollider>();
        collider.radius = 2.5f;
        hand.createColliders();
    }
}
