using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TYPE {
    HAND_CONFIGURATION,
    HEAD_CONFIGURATION,
    BODY_CONFIGURATION,
    MOVEMENT_DESCRIPTION,
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
    public Configuration configuration;

    private static Dictionary<TYPE, List<GROUP>> typeMap;

    private static void setTypeMap() {
        typeMap = new Dictionary<TYPE, List<GROUP>>();
        typeMap.Add(TYPE.HAND_CONFIGURATION, new List<GROUP>(){GROUP.INDEX, GROUP.INDEX_MIDDLE, GROUP.INDEX_MIDDLE_THUMB,
                                                               GROUP.FOUR_FINGERS, GROUP.FIVE_FINGERS, GROUP.BABY_FINGER, 
                                                               GROUP.RING_FINGER, GROUP.MIDDLE_FINGER, GROUP.INDEX_THUMB,
                                                               GROUP.THUMB});

        typeMap.Add(TYPE.HEAD_CONFIGURATION, new List<GROUP>(){GROUP.BROWS_EYES_EYEGAZE, GROUP.CHEEK_EARS_NOSE_BREATH,
                                                                GROUP.MOUTH_LIPS, GROUP.TONGUE_TEETH_CHIN_NECK, GROUP.HEAD});

        typeMap.Add(TYPE.BODY_CONFIGURATION, new List<GROUP>(){GROUP.SHOULDERS_HIPS_TORSO, GROUP.LIMBS});

        typeMap.Add(TYPE.MOVEMENT_DESCRIPTION, new List<GROUP>(){GROUP.CONTACT, GROUP.STRAIGHT_WALL_PLANE,
                                                                 GROUP.FINGER_MOVEMENT, GROUP.STRAIGHT_DIAGONAL_PLANE,
                                                                 GROUP.STRAIGHT_FLOOR_PLANE, GROUP.CURVES_WALL_PLANE,
                                                                 GROUP.CURVES_HIT_WALL_PLANE, GROUP.CURVES_HIT_FLOOR_PLANE,
                                                                 GROUP.CURVES_FLOOR_PLANE, GROUP.CIRCLE });

        typeMap.Add(TYPE.MOVEMENT_DYNAMIC, new List<GROUP>(){GROUP. DYNAMICS_TIMING, GROUP.PUNCTUATION,
                                                                 GROUP.LOCATION_FOR_SORTING });
    }
    
    public Symbol(int id, GROUP group) {
        this.id = id;
        this.group = group;
        this.type = getTypeByGroup(group);
        switch (type) {
            case TYPE.HAND_CONFIGURATION:
                this.configuration = new HandConfiguration();
                break;
            case TYPE.HEAD_CONFIGURATION:
                this.configuration = new HeadConfiguration();
                break;
            case TYPE.BODY_CONFIGURATION:
                this.configuration = new BodyConfiguration();
                break;
            case TYPE.MOVEMENT_DESCRIPTION:
                this.configuration = new MovementDescriptionConfiguration();
                break;
            case TYPE.MOVEMENT_DYNAMIC:
                this.configuration = new MovementDynamicConfiguration();
                break;
        }
    }

    private TYPE getTypeByGroup(GROUP group) {
        if (typeMap == null) { setTypeMap(); }
        foreach(TYPE type in typeMap.Keys){
            if(typeMap[type].Contains(group)){
                return type;
            }
        }        
        throw new System.Exception();
    }
   
    public void setupConfiguration(GameObject currentInterface) {
        configuration.setup(currentInterface);
    }

}
