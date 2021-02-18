using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EventScriptable", menuName = "GameJamHelper/EventList")]
public class EventScriptable : ScriptableObject {

    [SerializeField]
    public List<MessagesHelper> list = new List<MessagesHelper>();

}


