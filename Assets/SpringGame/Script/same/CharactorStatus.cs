using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactorStatus : MonoBehaviour,IDamageInterFace
{
    //体力
    public int maxHp = 10;
    public int playerHp;
    private Animator animator;
    public Slider playerHpBar;
    public GameObject gameOverCanvas;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerHp = maxHp;
        if (playerHpBar != null)
        {
            playerHpBar.value = playerHp;
        }
    }

    private void Update()
    {
        if (playerHp <= 0)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    void OnDie()
    {
        animator.SetTrigger("Die");
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