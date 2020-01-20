using UnityEngine;
public class BodyConfiguration : Configuration {
    public Vector3 rightShoulderPosition, leftShoulderPosition, spinePosition;
    public override void setup(GameObject currentInterface) {
        SetBodyPosition script = currentInterface.GetComponent<SetBodyPosition>();

        rightShoulderPosition = script.rightShoulderPosition;
        leftShoulderPosition = script.leftShoulderPosition;
        spinePosition = script.spinePosition;
    }
}
