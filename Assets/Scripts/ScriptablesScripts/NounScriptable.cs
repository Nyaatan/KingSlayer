using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouns", menuName = "GameJamHelper/NounList")]
public class NounScriptable : ScriptableObject
{
    [SerializeField]
    public List<string> nounList = new List<string>();
}
