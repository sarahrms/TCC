using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CaptureSystemController : MonoBehaviour {
    public Symbol symbol; 
    public AvatarCaptureSetup avatarSetupScript;
    public GameObject headTarget, spineTarget, rightArmTarget, leftArmTarget, rightHandTarget, leftHandTarget, 
        rightHandThumbTarget, rightHandIndexTarget, rightHandMiddleTarget, rightHandRingTarget, rightHandPinkyTarget,
        leftHandThumbTarget, leftHandIndexTarget, leftHandMiddleTarget, leftHandRingTarget, leftHandPinkyTarget;
    public bool draggingObject = false;
    
    void Start() {
        avatarSetupScript = GameObject.Find("Avatar").GetComponent<AvatarCaptureSetup>();
        symbol = new Symbol(0, GROUP.INDEX);
        StartCoroutine(WaitAndDoSomething());
    }
    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(0.25f);
        setTargetObjects();
        disableAllTargets();
        enableHandConfigurationTargets();
    }

    public TYPE changeGroup(int id, GROUP selectedGroup) {
        symbol = new Symbol(id, selectedGroup);
        return symbol.type;
    }

    public void save(GameObject currentInterface) {
        symbol.setupConfiguration(currentInterface);
        string filePath = Path.Combine("Assets\\Symbols\\" + symbol.type + "\\" + symbol.group + "\\", symbol.id.ToString("D3") + ".json");
        string jsonString = JsonUtility.ToJson(symbol);
        Debug.Log(jsonString);
        using (StreamWriter streamWriter = File.CreateText(filePath)) {
            streamWriter.Write(jsonString);
        }
    }

    public void setTargetObjects() {
        headTarget =  GameObject.Find("mixamorig:Head - Target");
        spineTarget = GameObject.Find("mixamorig:Spine - Target");

        rightArmTarget = GameObject.Find("mixamorig:RightArm - Target");
        leftArmTarget = GameObject.Find("mixamorig:LeftArm - Target");

        rightHandTarget = GameObject.Find("mixamorig:RightHand - Target");
        leftHandTarget = GameObject.Find("mixamorig:LeftHand - Target");

        rightHandThumbTarget = GameObject.Find("mixamorig:RightHandThumb1 - Target");
        rightHandIndexTarget = GameObject.Find("mixamorig:RightHandIndex1 - Target");
        rightHandMiddleTarget = GameObject.Find("mixamorig:RightHandMiddle1 - Target");
        rightHandRingTarget = GameObject.Find("mixamorig:RightHandRing1 - Target");
        rightHandPinkyTarget = GameObject.Find("mixamorig:RightHandPinky1 - Target");

        leftHandThumbTarget = GameObject.Find("mixamorig:LeftHandThumb1 - Target");
        leftHandIndexTarget = GameObject.Find("mixamorig:LeftHandIndex1 - Target");
        leftHandMiddleTarget = GameObject.Find("mixamorig:LeftHandMiddle1 - Target");
        leftHandRingTarget = GameObject.Find("mixamorig:LeftHandRing1 - Target");
        leftHandPinkyTarget = GameObject.Find("mixamorig:LeftHandPinky1 - Target");
    }

    public void disableAllTargets() {
        headTarget.SetActive(false);
        spineTarget.SetActive(false);

        rightArmTarget.SetActive(false);
        leftArmTarget.SetActive(false);

        rightHandTarget.SetActive(false);
        leftHandTarget.SetActive(false);

        rightHandThumbTarget.SetActive(false);
        rightHandIndexTarget.SetActive(false);
        rightHandMiddleTarget.SetActive(false);
        rightHandRingTarget.SetActive(false);
        rightHandPinkyTarget.SetActive(false);

        leftHandThumbTarget.SetActive(false);
        leftHandIndexTarget.SetActive(false);
        leftHandMiddleTarget.SetActive(false);
        leftHandRingTarget.SetActive(false);
        leftHandPinkyTarget.SetActive(false);
    }
    public void enableHandConfigurationTargets() {
        rightHandThumbTarget.SetActive(true);
        rightHandIndexTarget.SetActive(true);
        rightHandMiddleTarget.SetActive(true);
        rightHandRingTarget.SetActive(true);
        rightHandPinkyTarget.SetActive(true);
    }

    public void reset() {
        if(avatarSetupScript!=null){
            avatarSetupScript.reset();
        }
    }
    public void enableBodyConfigurationTargets() {
        spineTarget.SetActive(true);
        rightArmTarget.SetActive(true);
        leftArmTarget.SetActive(true);
    }

    public void enableHeadConfigurationTargets() {
        headTarget.SetActive(true);
    }

    public void enableMovementConfigurationTargets(GROUP selectedGroup) {
        switch (selectedGroup) {
            case GROUP.FINGER_MOVEMENT:
                enableHandConfigurationTargets();
                break;

            case GROUP.HEAD:
                headTarget.SetActive(true);
                break;

            default:
                rightHandTarget.SetActive(true);
                break;
        }
    }

}
