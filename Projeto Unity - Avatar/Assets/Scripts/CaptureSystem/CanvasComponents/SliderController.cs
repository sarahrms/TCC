using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour {
    public Transform handTarget;

    public void Start() {
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        init();
    }

    public void init() {
        //handTarget 
    }
    public void onChange() {
        
    }
}
