using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventScriptable", menuName = "GameJamHelper/CharacterList")]
public class CharacterScriptable : ScriptableObject
{
    [SerializeField]
    public List<AvatarHelper> avaList = new List<AvatarHelper>();
}
