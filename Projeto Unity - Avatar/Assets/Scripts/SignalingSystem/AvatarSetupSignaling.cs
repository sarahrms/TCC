using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetupSignaling : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public BodyComponent body;

    public void init() {
        ikScript = gameObject.GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        body = new BodyComponent(GameObject.Find("mixamorig:Spine").transform);
        body.setIkTargets(ikScript);
    }

    public void Update() {
        body.update();
    }

    public void reset() {
        body.reset();
    }

}
