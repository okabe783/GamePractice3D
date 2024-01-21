using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int hp = 100;
    public int MaxHp = 100;

    public GameObject lastAttackTarget = null;

    public bool attacking = false;
    public bool died = false;
}
