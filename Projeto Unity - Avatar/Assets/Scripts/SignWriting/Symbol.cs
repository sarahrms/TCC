using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TYPE {
    HAND_CONFIGURATION,
    FACE_CONFIGURATION,
    BODY_CONFIGURATION,
    MOVEMENT_CONFIGURATION,
    MOVEMENT_DYNAMIC
}

[System.Serializable]
public enum GROUP {
    INDEX,
    INDEX_MIDDLE,
    INDEX_MIDDLE_THUMB,
    FOUR_FINGERS,
    FIVE_FINGERS,
    BABY_FINGER,
    RING_FINGER,
    MIDDLE_FINGER,
    INDEX_THUMB,
    THUMB,
    CONTACT,
    FINGER_MOVEMENT,
    STRAIGHT_WALL_PLANE,
    STRAIGHT_DIAGONAL_PLANE,
    STRAIGHT_FLOOR_PLANE,
    CURVES_WALL_PLANE,
    CURVES_HIT_WALL_PLANE,
    CURVES_HIT_FLOOR_PLANE,
    CURVES_FLOOR_PLANE,
    CIRCLE,
    BROWS_EYES_EYEGAZE,
    CHEEK_EARS_NOSE_BREATH,
    MOUTH_LIPS,
    TONGUE_TEETH_CHIN_NECK,
    HEAD,
    SHOULDERS_HIPS_TORSO,
    LIMBS,
    DYNAMICS_TIMING,
    PUNCTUATION,
    LOCATION_FOR_SORTING,
}

[System.Serializable]
public class Symbol {
    public int id;
    public TYPE type;
    public GROUP group;
    public string configuration;

    private Configuration configurationObj;

    public static Dictionary<TYPE, List<GROUP>> typeMap;

    public static void setTypeMap() {
        typeMap = new Dictionary<TYPE, List<GROUP>>();
        typeMap.Add(TYPE.HAND_CONFIGURATION, new List<GROUP>(){GROUP.INDEX, 
                                                               GROUP.INDEX_MIDDLE, 
                                                               GROUP.INDEX_MIDDLE_THUMB,
                                                               GROUP.FOUR_FINGERS, 
                                                               GROUP.FIVE_FINGERS, 
                                                               GROUP.BABY_FINGER, 
                                                               GROUP.RING_FINGER, 
                                                               GROUP.MIDDLE_FINGER, 
                                                               GROUP.INDEX_THUMB,
                                                               GROUP.THUMB});

        typeMap.Add(TYPE.FACE_CONFIGURATION, new List<GROUP>(){GROUP.BROWS_EYES_EYEGAZE, 
                                                               GROUP.CHEEK_EARS_NOSE_BREATH,
                                                               GROUP.MOUTH_LIPS, 
                                                               GROUP.TONGUE_TEETH_CHIN_NECK});

        typeMap.Add(TYPE.BODY_CONFIGURATION, new List<GROUP>(){GROUP.SHOULDERS_HIPS_TORSO, 
                                                               GROUP.LIMBS});

        typeMap.Add(TYPE.MOVEMENT_CONFIGURATION, new List<GROUP>(){GROUP.CONTACT,
                                                                 GROUP.STRAIGHT_WALL_PLANE,
                                                                 GROUP.CURVES_FLOOR_PLANE, 
                                                                 GROUP.STRAIGHT_DIAGONAL_PLANE,
                                                                 GROUP.STRAIGHT_FLOOR_PLANE, 
                                                                 GROUP.CURVES_WALL_PLANE,
                                                                 GROUP.CURVES_HIT_WALL_PLANE, 
                                                                 GROUP.CURVES_HIT_FLOOR_PLANE,
                                                                 GROUP.CIRCLE, 
                                                                 GROUP.HEAD, 
                                                                 GROUP.FINGER_MOVEMENT});

        typeMap.Add(TYPE.MOVEMENT_DYNAMIC, new List<GROUP>(){GROUP. DYNAMICS_TIMING, 
                                                             GROUP.PUNCTUATION,
                                                             GROUP.LOCATION_FOR_SORTING});
    }
    
    public Symbol(int id, GROUP group) {
        this.id = id;
        this.group = group;
        type = getTypeByGroup(group);
        switch (type) {
            case TYPE.HAND_CONFIGURATION:
                configurationObj = new HandConfiguration();
                break;
            case TYPE.FACE_CONFIGURATION:
                configurationObj = new FaceConfiguration();
                break;
            case TYPE.BODY_CONFIGURATION:
                configurationObj = new BodyConfiguration();
                break;
            case TYPE.MOVEMENT_CONFIGURATION:
                MovementConfiguration movementConfiguration = new MovementConfiguration();
                movementConfiguration.type = getMovementType();
                configurationObj = movementConfiguration;
                break;
            case TYPE.MOVEMENT_DYNAMIC:
                configurationObj = new DynamicsConfiguration();
                break;
        }
    }

    public MOVEMENT_TYPE getMovementType() {
        if(group == GROUP.FINGER_MOVEMENT) {
            return MOVEMENT_TYPE.FINGERS_MOVEMENT;
        }
        else if(group == GROUP.HEAD) {
            return MOVEMENT_TYPE.HEAD_MOVEMENT;
        }
        return MOVEMENT_TYPE.HANDS_MOVEMENT;
    }

    private TYPE getTypeByGroup(GROUP group) {
        foreach(TYPE type in typeMap.Keys){
            if(typeMap[type].Contains(group)){
                return type;
            }
        }        
        throw new System.Exception();
    }
   
    public void setupConfiguration(GameObject currentInterface) {
        configurationObj.setup(currentInterface);
        configuration = setConfigurationData();
    }

    public string setConfigurationData() {
        switch (type) {
            case TYPE.HAND_CONFIGURATION:
                return JsonUtility.ToJson((HandConfiguration) configurationObj);
            case TYPE.FACE_CONFIGURATION:
                return JsonUtility.ToJson((FaceConfiguration) configurationObj);
            case TYPE.BODY_CONFIGURATION:
                return JsonUtility.ToJson((BodyConfiguration) configurationObj);
            case TYPE.MOVEMENT_CONFIGURATION:
                return JsonUtility.ToJson((MovementConfiguration) configurationObj);
            case TYPE.MOVEMENT_DYNAMIC:
                return JsonUtility.ToJson((DynamicsConfiguration) configurationObj);
            default: 
                throw new System.Exception();
        }
    }

}
