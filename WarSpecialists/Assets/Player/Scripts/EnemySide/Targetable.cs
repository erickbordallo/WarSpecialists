﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public enum EnemyType { Minion, Tower, Champion }
    public EnemyType enemyType;
    public GameTypes.Team team = GameTypes.Team.Blue;
}
