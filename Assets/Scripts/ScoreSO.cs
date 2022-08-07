using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scoret Type", menuName = "ScoreSO")]
public class ScoreSO : ScriptableObject
{
    [SerializeField] private int _gameLevel = 0;
    public int gameLevel
    {
        get { return _gameLevel; }
        set { _gameLevel = value; }
    }
}
