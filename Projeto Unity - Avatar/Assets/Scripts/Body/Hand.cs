using System.Collections.Generic;
using UnityEngine;

public class Hand {
    public Transform wrist;
    public List<Finger> fingers;
   public Hand(Transform wrist) {
        this.wrist = wrist;
        for(int i=0; i < 5; i++) {
            Finger finger = new Finger(wrist.GetChild(i).GetChild(0).GetChild(0).GetChild(0).transform);
            fingers.Add(finger);
        }
   }
}
