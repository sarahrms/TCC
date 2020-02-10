using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

public class Term {
    public string name;

    public virtual void save() {  
        string filePath = Path.Combine("Assets\\Terms\\" + name + ".json");
        string jsonString = JsonUtility.ToJson(this);
        Debug.Log(jsonString);
        using (StreamWriter streamWriter = File.CreateText(filePath)) {
            streamWriter.Write(jsonString);
        }
    }
}

[System.Serializable]

public class SimpleTerm : Term { 
    public string rightHandConfigurationPath, leftHandConfigurationPath;
    public string faceConfigurationPath;
    public string bodyConfigurationPath;
    public string rightHandMovementConfigurationPath, leftHandMovementConfigurationPath;
    public bool overwriteRightHand, overwriteLeftHand;
    public Vector3 rightHandPosition, leftHandPosition;    
    public Vector3 rightHandRotation, leftHandRotation;
    public string rightHandFingersMovementConfigurationPath, leftHandFingersMovementConfigurationPath;
    public string headMovementConfigurationPath;
}

[System.Serializable]

public class CompositeTerm : Term {
    public List<string> termList;   
}
