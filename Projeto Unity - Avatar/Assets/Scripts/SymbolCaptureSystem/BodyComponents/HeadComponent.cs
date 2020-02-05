using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadComponent : BasicBodyComponent {
    public Transform head, headTarget;
    public Vector3 initialHeadPosition;
    public Quaternion initialHeadRotation;
    public float radius = 1.5f;
    public RootMotion.FinalIK.FullBodyBipedIK ikScript;

    public HeadComponent(Transform headTransform) {
        head = headTransform;
        initialHeadPosition = headTransform.position;
        initialHeadRotation = headTransform.rotation;
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
        RootMotion.FinalIK.IKSolver.Bone[] bones = new RootMotion.FinalIK.IKSolver.Bone[2];

        bones[0] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[0].transform = neck;
        bones[1] = new RootMotion.FinalIK.IKSolver.Bone();
        bones[1].transform = head;

        headTarget = new GameObject().transform;
        headTarget.name = head.name + " - Target";
        headTarget.position = head.position;
        headTarget.rotation = head.rotation;
        headTarget.localScale = head.localScale;
        headTarget.SetParent(neck);

        RootMotion.FinalIK.FABRIK script = neck.gameObject.AddComponent<RootMotion.FinalIK.FABRIK>();
        script.solver.target = headTarget;
        script.solver.IKPositionWeight = 1;
        script.solver.bones = bones;
        script.enabled = true;
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
