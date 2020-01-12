using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCaptureController : MonoBehaviour {
    public Symbol symbol; 
    public AvatarSetup avatarSetupScript;
    public GameObject selectedObject;
    // Start is called before the first frame update
    void Start() {
        avatarSetupScript = GameObject.Find("Avatar").GetComponent<AvatarSetup>();
        avatarSetupScript.init();
        avatarSetupScript.setupSignCapture();
    }

    public void setSelectedObject(GameObject selected) {
        this.selectedObject = selected;
    }

   public void rotateSelectedObject(int degree) {
       // selectedObject.GetComponent<Transform>().
    }

    // Update is called once per frame
    void Update(){
        
    }

    public TYPE changeGroup(int id, GROUP selectedGroup) {
        symbol = new Symbol(id, selectedGroup);
        return symbol.type;
    }

    public void save() {
        symbol.save();
    }
}
