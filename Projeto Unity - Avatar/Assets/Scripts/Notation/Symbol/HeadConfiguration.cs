using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HeadConfiguration : Configuration {
    public Vector3 headPosition, headRotation;

    public void setup(SetHeadPosition script) {
        headPosition = script.headPosition;
        headRotation = script.headRotation;
    }
}
