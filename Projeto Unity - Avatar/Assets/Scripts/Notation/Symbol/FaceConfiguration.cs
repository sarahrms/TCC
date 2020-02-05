using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class FaceConfiguration : Configuration {
    public string animation;
    public override void setup(GameObject currentInterface) {
        animation = currentInterface.transform.GetChild(1).GetComponent<InputField>().text;
    }
}
