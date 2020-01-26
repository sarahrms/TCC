using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HandConfiguration : Configuration {
    public List<Vector3> positions = new List<Vector3>();
    public override void setup(GameObject currentInterface) {
        SetFingersPosition script = currentInterface.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SetFingersPosition>();

        positions.Add(script.indexPosition);
        positions.Add(script.middlePosition);
        positions.Add(script.pinkyPosition);
        positions.Add(script.ringPosition);
        positions.Add(script.thumbPosition);
    }

    public void setup(SetFingersPosition script) {
        positions.Add(script.indexPosition);
        positions.Add(script.middlePosition);
        positions.Add(script.pinkyPosition);
        positions.Add(script.ringPosition);
        positions.Add(script.thumbPosition);
    }
}
