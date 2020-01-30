using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVEMENT_TYPE {
    FINGERS_MOVEMENT,
    HANDS_MOVEMENT, 
    HEAD_MOVEMENT
}

public class MovementConfiguration : Configuration {
    public MOVEMENT_TYPE type;
    public List<string> configurations;
    private List<Configuration> configurationList;
    public override void setup(GameObject currentInterface) {
        configurationList = new List<Configuration>();
        Transform positionAggregator;
        positionAggregator = currentInterface.transform.GetChild(1);

        switch (type) {   
            case MOVEMENT_TYPE.HANDS_MOVEMENT: 
                for (int i = 0; i < positionAggregator.childCount; i++) {
                    SetHandPosition script = positionAggregator.GetChild(i).GetComponent<SetHandPosition>();
                    WristConfiguration positionConfiguration = new WristConfiguration();
                    positionConfiguration.setup(script);
                    configurationList.Add(positionConfiguration);
                }
                break;
            case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                for (int i = 0; i < positionAggregator.childCount; i++) {
                    SetFingersPosition script = positionAggregator.GetChild(i).GetComponent<SetFingersPosition>();
                    HandConfiguration handConfiguration = new HandConfiguration();
                    handConfiguration.setup(script);
                    configurationList.Add(handConfiguration);
                }
                break;
            case MOVEMENT_TYPE.HEAD_MOVEMENT:
                for (int i = 0; i < positionAggregator.childCount; i++) {
                    SetHeadPosition script = positionAggregator.GetChild(i).GetComponent<SetHeadPosition>();
                    HeadConfiguration positionConfiguration = new HeadConfiguration();
                    positionConfiguration.setup(script);
                    configurationList.Add(positionConfiguration);
                }
                break;
            default:
                throw new System.Exception();
        }
        setupPositionsData();
    }

    public void setupPositionsData() {
        configurations = new List<string>();
        switch (type) {
            case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                foreach(Configuration config in configurationList) {
                    configurations.Add(JsonUtility.ToJson((HandConfiguration) config));
                }
                break;
            case MOVEMENT_TYPE.HANDS_MOVEMENT:
                foreach (Configuration config in configurationList) {
                    configurations.Add(JsonUtility.ToJson((WristConfiguration)config));
                }
                break;
            case MOVEMENT_TYPE.HEAD_MOVEMENT:
                foreach (Configuration config in configurationList) {
                    configurations.Add(JsonUtility.ToJson((HeadConfiguration)config));
                }
                break;
            default:
                throw new System.Exception();
        }
    }
}
