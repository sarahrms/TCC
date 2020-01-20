using UnityEngine;

[System.Serializable]

public class HandConfiguration : Configuration {
    public Vector3 indexPosition, middlePosition, thumbPosition, pinkyPosition, ringPosition;
    public override void setup(GameObject currentInterface) {
        SetFingersPosition script = currentInterface.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SetFingersPosition>();
       
        indexPosition = script.indexPosition;
        middlePosition = script.middlePosition;
        thumbPosition = script.thumbPosition;
        pinkyPosition = script.pinkyPosition;
        ringPosition = script.pinkyPosition;
    }

    public void setup(SetFingersPosition script) {
        indexPosition = script.indexPosition;
        middlePosition = script.middlePosition;
        thumbPosition = script.thumbPosition;
        pinkyPosition = script.pinkyPosition;
        ringPosition = script.pinkyPosition;
    }
}
