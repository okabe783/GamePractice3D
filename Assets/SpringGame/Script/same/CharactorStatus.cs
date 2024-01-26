using UnityEngine;

public class CharactorStatus : MonoBehaviour
{
    //体力
    public int hp = 100;
    public int maxHp = 100;
    //攻撃力
    public int power = 10;
    //最後に攻撃した対象
    public GameObject lastAttackTarget = null;
    //player名
    public string name = "Player";
    //状態
    public bool attacking = false;
    public bool died = false;
}