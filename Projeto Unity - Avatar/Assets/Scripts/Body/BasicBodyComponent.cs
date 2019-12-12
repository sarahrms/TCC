using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBodyComponent{
    
    public void createGizmo(Transform target, float radius, Color color){
        GameObject sphereGizmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereGizmo.transform.SetParent(target);
        sphereGizmo.transform.localPosition = new Vector3(0, 0, 0);
        sphereGizmo.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        sphereGizmo.GetComponent<SphereCollider>().enabled = false;
        sphereGizmo.GetComponent<MeshRenderer>().materials = new Material[] { Resources.Load("SphereMaterial") as Material };
        sphereGizmo.GetComponent<MeshRenderer>().material.color = color;
    }

    public void reset(Transform target, Transform initial) {
        target.transform.position = initial.position;
        target.transform.rotation = initial.rotation;
        target.transform.localScale = initial.localScale;
    }

    public void createCollider(float radius, GameObject target) {
        SphereCollider collider = target.AddComponent<SphereCollider>();
        collider.radius = radius;
    }
    public void setMouseDrag(Transform bodyPart, GameObject ikTarget) {
        MouseDragTargeting mouseDrag = ikTarget.AddComponent<MouseDragTargeting>();
        mouseDrag.dragTarget = bodyPart.gameObject;
    }
   
}
