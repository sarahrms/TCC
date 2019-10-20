using UnityEngine;
public class Arm {
    public Transform shoulder;
    public Hand hand;

    public Arm(Transform shoulder) {
        this.shoulder = shoulder;
        this.hand = new Hand(shoulder.GetChild(0).GetChild(0).transform);
    }
}
