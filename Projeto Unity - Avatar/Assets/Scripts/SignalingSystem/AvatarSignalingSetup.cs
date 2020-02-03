﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSignalingSetup : MonoBehaviour {
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;
    public BodyController bodyController;

    public void init() {
        ikScript = gameObject.GetComponent<RootMotion.FinalIK.FullBodyBipedIK>();
        bodyController = new BodyController(GameObject.Find("mixamorig:Spine").transform);
        bodyController.setIkTargets(ikScript);
    }

    public void update() {
       bodyController.update();
    }

    public void reset() {
        bodyController.reset();
    }

}