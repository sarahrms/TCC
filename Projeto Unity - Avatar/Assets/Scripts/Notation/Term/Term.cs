using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Term {
    public string name;
    public BodyData bodyData;

    public virtual void save() {  
        string filePath = "Assets\\Terms\\" + name + ".json";
        string jsonString = JsonUtility.ToJson(this);
        Debug.Log(jsonString);
        using (StreamWriter streamWriter = File.CreateText(filePath)) {
            streamWriter.Write(jsonString);
        }
    }
}

[System.Serializable]
public class Word {
    public string name;
    public List<string> termList;

    public virtual void save() {
        string filePath = "Assets\\Words\\" + name + ".json";
        string jsonString = JsonUtility.ToJson(this);
        Debug.Log(jsonString);
        using (StreamWriter streamWriter = File.CreateText(filePath)) {
            streamWriter.Write(jsonString);
        }
    }
}

[System.Serializable]
public class BodyData {
    public string faceConfigurationPath;
    public string headMovementConfigurationPath;
    public string bodyConfigurationPath;
    public ArmData rightArm, leftArm;
}

[System.Serializable]
public class ArmData {
    public bool overwriteHand;
    public Vector3 handPosition, handRotation;
    public string handMovementConfigurationPath;
    public HandData handData;
}

[System.Serializable]
public class HandData {
    public string handConfigurationPath;
    public string fingersMovementConfigurationPath;
}
