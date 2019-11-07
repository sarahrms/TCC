using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public Body body;

    void Start(){        
        ikScript = gameObject.GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        body = new Body(GameObject.Find("mixamorig:Spine").transform);
        body.setIkTargets(ikScript);
        body.createColliders();
        body.setMouseDrag();
      //  body.createGizmo();
    }

     public void reset() {
         body.reset();
     }

    private void Update() {
        
    }

}
