using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SignCaptureController : MonoBehaviour {
    public Symbol symbol; 
    public AvatarSetup avatarSetupScript;
    public GameObject selectedObject;
    public bool draggingObject = false;
    
    void Start() {
        symbol = new Symbol(0, GROUP.INDEX);
        avatarSetupScript = GameObject.Find("Avatar").GetComponent<AvatarSetup>();
        avatarSetupScript.init();
        avatarSetupScript.setupSignCapture();
    }
    public TYPE changeGroup(int id, GROUP selectedGroup) {
        symbol = new Symbol(id, selectedGroup);
        return symbol.type;
    }

    public void save(GameObject currentInterface) {
        symbol.setupConfiguration(currentInterface);
        string filePath = Path.Combine("Resources\\" + symbol.type + "\\" + symbol.group + "\\", symbol.id + ".json");
        string jsonString = JsonUtility.ToJson(symbol);
        using (StreamWriter streamWriter = File.CreateText(filePath)) {
            streamWriter.Write(jsonString);
        }
    }
}
