using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum MOVEMENT_TYPE {
    FINGERS_MOVEMENT,
    HANDS_MOVEMENT, 
    HEAD_MOVEMENT
}

public enum TRAJECTORY_PLANE {
    XY,
    XZ,
    YZ
}

public enum TRAJECTORY_DIRECTION {
    CLOCK_WISE,
    COUNTER_CLOCK_WISE
}

public class MovementConfiguration : Configuration {
    public MOVEMENT_TYPE movementType;
    public TRAJECTORY_TYPE trajectoryType;
    public TRAJECTORY_PLANE trajectoryPlane;
    public List<TRAJECTORY_DIRECTION> trajectoryDirections;
    public List<string> configurations;
    public List<Configuration> configurationList;
    public override void setup(GameObject currentInterface) {
        trajectoryDirections =  new List<TRAJECTORY_DIRECTION>();
        configurationList = new List<Configuration>();
        Transform positionAggregator;
        positionAggregator = currentInterface.transform.GetChild(1);

        switch (movementType) {   
            case MOVEMENT_TYPE.HANDS_MOVEMENT: 
                for (int i = 0; i < positionAggregator.childCount; i++) {
                    SetHandPosition script = positionAggregator.GetChild(i).GetComponent<SetHandPosition>();
                    WristConfiguration positionConfiguration = new WristConfiguration();
                    positionConfiguration.setup(script);
                    configurationList.Add(positionConfiguration);
                    switch (positionAggregator.GetChild(i).GetChild(3).GetComponent<Dropdown>().value) {
                        case 0:
                            trajectoryDirections.Add(TRAJECTORY_DIRECTION.CLOCK_WISE);
                            break;
                        case 1:
                            trajectoryDirections.Add(TRAJECTORY_DIRECTION.COUNTER_CLOCK_WISE);
                            break;
                    }
                }
                switch (currentInterface.transform.GetChild(3).GetComponent<Dropdown>().value) {
                    case 0:
                        trajectoryPlane = TRAJECTORY_PLANE.XY;
                        break;
                    case 1:
                        trajectoryPlane = TRAJECTORY_PLANE.XZ;
                        break;
                    case 2:
                        trajectoryPlane = TRAJECTORY_PLANE.YZ;
                        break;
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
        switch (movementType) {
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

    public void loadConfigurationList() {
        configurationList = new List<Configuration>();
        switch (movementType) {
            case MOVEMENT_TYPE.FINGERS_MOVEMENT:
                foreach(string jsonString in configurations) {
                    configurationList.Add(JsonUtility.FromJson<HandConfiguration>(jsonString));
                }
                break;
            case MOVEMENT_TYPE.HANDS_MOVEMENT:
                foreach (string jsonString in configurations) {
                    configurationList.Add(JsonUtility.FromJson<WristConfiguration>(jsonString));
                }
                break;
            case MOVEMENT_TYPE.HEAD_MOVEMENT:
                foreach (string jsonString in configurations) {
                    configurationList.Add(JsonUtility.FromJson<HeadConfiguration>(jsonString));
                }
                break;
            default:
                throw new System.Exception();
        }
    }
}
