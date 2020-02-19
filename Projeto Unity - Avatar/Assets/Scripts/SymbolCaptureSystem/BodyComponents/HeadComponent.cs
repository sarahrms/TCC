using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadComponent : BasicBodyComponent {
    public Transform spineRoot, spine, neck, head, headTarget;
    public Vector3 initialHeadPosition;
    public Quaternion initialHeadRotation;
    public float radius = 1.5f;
    public RootMotion.FinalIK.LimbIK ikScript;

    public HeadComponent(Transform headTransform) {        
        head = headTransform;
        neck = head.parent;
        spine = neck.parent;
        spineRoot = spine.parent;

        initialHeadPosition = head.position;
        initialHeadRotation = head.rotation;
        setLine();
    }

    public void update() {
        drawLine(headTarget.position, head.position);
    }

    public void reset() {
        headTarget.position = initialHeadPosition;
        headTarget.rotation = initialHeadRotation;
    }

    public void setIkTargets(Transform neck) {
        headTarget = new GameObject().transform;
        headTarget.name = head.name + " - Target";
        headTarget.position = head.position;
        headTarget.rotation = head.rotation;
        headTarget.localScale = head.localScale;
        headTarget.SetParent(neck);

        ikScript = spineRoot.gameObject.AddComponent<RootMotion.FinalIK.LimbIK>();
        ikScript.solver.target = headTarget;
        ikScript.solver.IKPositionWeight = 1;
        ikScript.solver.IKRotationWeight = 1;
        ikScript.solver.SetChain(spineRoot, spine, neck, head);
        ikScript.enabled = true;
    }

    public void createGizmo() {
        createGizmo(headTarget, radius);
    }

    public void createCollider() {
        createCollider(radius, headTarget.gameObject);
    }

    public void setMouseDrag() {
        setMouseDrag(headTarget.gameObject);
    }

}
