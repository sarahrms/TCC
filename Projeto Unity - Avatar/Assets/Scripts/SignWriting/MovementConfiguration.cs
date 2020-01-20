using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVEMENT_TYPE {
    FINGERS_MOVEMENT,
    HANDS_MOVEMENT
}

public class MovementConfiguration : Configuration {
    public MOVEMENT_TYPE type;
    public int vezes;
    public List<string> configurations;
    private List<Configuration> configurationList;
    public override void setup(GameObject currentInterface) {
        Transform positionAggregator;
        switch (type) {
            case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                positionAggregator = currentInterface.transform.GetChild(3).GetChild(1);
                configurationList = new List<Configuration>();
                for (int i = 0; i < positionAggregator.childCount; i++) {
                    SetFingersPosition script = positionAggregator.GetChild(i).GetComponent<SetFingersPosition>();
                    HandConfiguration handConfiguration = new HandConfiguration();
                    handConfiguration.setup(script);
                    configurationList.Add(handConfiguration);
                }
                break;
            case MOVEMENT_TYPE.HANDS_MOVEMENT:
                positionAggregator = currentInterface.transform.GetChild(2).GetChild(1);                
                for (int i = 0; i < positionAggregator.childCount; i++) {
                    SetHandPosition script = positionAggregator.GetChild(i).GetComponent<SetHandPosition>();
                    WristConfiguration positionConfiguration = new WristConfiguration();
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
            default:
                throw new System.Exception();
        }
    }
}
