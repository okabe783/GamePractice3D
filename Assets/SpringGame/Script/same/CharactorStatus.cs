using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharactorStatus : MonoBehaviour,IDamageInterFace
{
    //体力
    public int maxHp = 100;
    public int playerHp;
    private Animator animator;
    public Slider playerHpBar;

    public UnityEvent onDieCallback = new UnityEvent();

    void Start()
    {
        animator = GetComponent<Animator>();
        playerHp = maxHp;
        if (playerHpBar != null)
        {
            playerHpBar.value = playerHp;
        }
    }
    
    void OnDie()
    {
        animator.SetTrigger("Die");
        onDieCallback.Invoke();
    }

    public void AtDamage(int damageValue)
    {
        if (playerHp <= 0)
        {
            return;
        }

        playerHp -= damageValue;
        //UI
        if (playerHpBar != null)
        {
            playerHpBar.value = playerHp;
        }
        if (playerHp <= 0)
        {
            OnDie();
        }
    }
}