using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupAvatar : MonoBehaviour {
    private RootMotion.FinalIK.FullBodyBipedIK ikScript;
    private Body body;

    void Start(){
        body = new Body(GameObject.Find("mixamorig:Spine").transform);
        
        ikScript = gameObject.AddComponent<RootMotion.FinalIK.FullBodyBipedIK>() as RootMotion.FinalIK.FullBodyBipedIK;
        body.setIkTargets(ikScript);
        body.createColliders();
        body.setMouseDrag();
        
    }
   // GameObject skeleton = transform.GetChild(2).gameObject;

    void Update(){
        
    }
}
