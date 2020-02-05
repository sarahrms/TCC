using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBodyComponent {
    private GameObject myLine;
    private LineRenderer lineRenderer; 

    public void setLine() {
        myLine = new GameObject();
        myLine.transform.SetParent(GameObject.Find("LineAggregator").transform);
        myLine.name = "Line " + myLine.transform.parent.childCount;
        lineRenderer = myLine.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.startWidth = 0.1f;
    }

    public void drawLine(Vector3 initialPosition, Vector3 finalPosition) {
        lineRenderer.SetPosition(0, initialPosition);
        lineRenderer.SetPosition(1, finalPosition);
    }
    
    public void createGizmo(Transform target, float radius){
        GameObject sphereGizmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereGizmo.transform.SetParent(target);
        sphereGizmo.transform.localPosition = new Vector3(0, 0, 0);
        sphereGizmo.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        sphereGizmo.GetComponent<SphereCollider>().enabled = false;
        sphereGizmo.GetComponent<MeshRenderer>().materials = new Material[] { Resources.Load("SphereMaterial") as Material };
    }

    public void createCollider(float radius, GameObject target) {
        SphereCollider collider = target.AddComponent<SphereCollider>();
        collider.radius = radius;
    }
    public void setMouseDrag(GameObject ikTarget) {
        ikTarget.AddComponent<MouseDragTargeting>();
    }
   
}
