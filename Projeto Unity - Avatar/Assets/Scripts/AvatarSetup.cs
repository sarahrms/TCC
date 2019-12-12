using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public Body body;

    public void Start(){        
        ikScript = gameObject.GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        body = new Body(GameObject.Find("mixamorig:Spine").transform);
        body.setIkTargets(ikScript);
    }

    public void setupSignCapture() {
        body.createColliders();
        body.setMouseDrag();
        body.createGizmo();
    }

    public void setupSignaling() {

    }

    public void reset() {
        body.reset();
    }


}
