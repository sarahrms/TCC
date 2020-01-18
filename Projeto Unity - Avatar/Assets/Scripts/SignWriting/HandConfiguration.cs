using UnityEngine;

public class HandConfiguration : Configuration{
    public Vector3 indexTargetPosition, middleTargetPosition, thumbTargetPosition, pinkyTargetPosition,
        ringTargetPosition;
    public override void setup(GameObject currentInterface) {
        SetFingerPosition script = currentInterface.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SetFingerPosition>();
       
        indexTargetPosition = script.indexPosition;
        middleTargetPosition = script.middlePosition;
        thumbTargetPosition = script.thumbPosition;
        pinkyTargetPosition = script.pinkyPosition;
        ringTargetPosition = script.pinkyPosition;
    }
}
