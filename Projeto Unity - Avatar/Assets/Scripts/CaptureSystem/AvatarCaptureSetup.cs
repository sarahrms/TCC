﻿using UnityEngine;

public class AvatarCaptureSetup : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public BodyComponent body;

    public void Start(){        
        ikScript = gameObject.GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        body = new BodyComponent(GameObject.Find("mixamorig:Spine").transform);
        body.setIkTargets(ikScript);
        body.createGizmo();
        body.createCollider();
        body.setMouseDrag();
    }

    public void FixedUpdate() {
        body.update();
    }

    public void reset() {
        body.reset();
    }

}