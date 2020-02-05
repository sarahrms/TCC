using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristConfiguration : Configuration {
    public Vector3 handPosition, handRotation;

    public void setup(SetHandPosition script) {
        handPosition = script.handPosition;
        handRotation = script.handRotation;
    }

}
