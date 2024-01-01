using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SceneItems
{
    None,
    H1,
    H2,
    H2A,
    H3,
    H4
};

public class Scenes: Singleton<Scenes>
{
    public string GetSceneName(SceneItems item)
    {
        string name = null;

        switch (item)
        {
            case SceneItems.None:
                name = string.Empty;
                break;
            case SceneItems.H1:
                name = "H1";
                break;
            case SceneItems.H2:
                name = "H2";
                break;
            case SceneItems.H2A:
                name = "H2A";
                break;
            case SceneItems.H3:
                name = "H3";
                break;
            case SceneItems.H4:
                name = "H4";
                break;
        }

        return name;
    }
}