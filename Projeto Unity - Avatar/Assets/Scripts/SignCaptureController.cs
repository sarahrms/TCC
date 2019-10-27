using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCaptureController : MonoBehaviour {
    public Symbol symbol; 
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void changeGroup(int id, GROUP selectedGroup) {
        symbol = new Symbol(id, selectedGroup);
        switch (symbol.type) {
            case TYPE.HAND_CONFIGURATION:


                break;
            case TYPE.MOVEMENT_DESCRIPTION:


                break;
            case TYPE.BODY_CONFIGURATION:


                break;
        }
    }
}
