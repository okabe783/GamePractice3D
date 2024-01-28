using UnityEngine;
using UnityEngine.Events;

public class CharactorStatus : MonoBehaviour
{
    //体力
    public int maxHp = 100;
    public int hp;
    private Animator animator;

    public UnityEvent onDieCallback = new UnityEvent();

    void Start()
    {
        animator = GetComponent<Animator>();
        hp = maxHp;
    }

    public void Damage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }

        hp -= damage;
        //UIはここに書く
        if (hp <= 0)
        {
            OnDie();
        }
    }

    void OnDie()
    {
        animator.SetBool("Die",true);
        onDieCallback.Invoke();
    }
}