using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Term {
    public string name;
}
public class SimpleTerm : Term { 
    public string rightHandConfigurationPath, idLeftHandConfigurationPath;
    public string faceConfigurationPath;
    public string bodyConfigurationPath;
    public string rightHandMovementConfigurationPath, idLeftHandMovementConfigurationPath;
    public bool overwriteRightHand, overwriteLeftHand;
    public Vector3 rightHandPosition, leftHandPosition;    
    public Vector3 rightHandRotation, leftHandRotation;
    public int rightHandFingersMovementConfigurationPath, idLeftHandFingersMovementConfigurationPath;
    public int headConfigurationPath;
}

public class CompositeTerm : Term {
    List<Term> terms;
}
