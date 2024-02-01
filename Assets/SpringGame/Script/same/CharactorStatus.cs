using UnityEngine;
using UnityEngine.UI;

public class CharactorStatus : MonoBehaviour,IDamageInterFace
{
    //体力
    public int maxHp = 10;
    public int playerHp;
    private Animator animator;
    public Slider playerHpBar;
    public GameObject gameOverCanvas;
    public GameObject gameWinCanvas;
    public GameObject enemy;
    private CharactorMove charaMove;

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

        Win();
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

    public void Win()
    {
        if (!enemy)
        {
            gameWinCanvas.SetActive(true);
            Debug.Log("Win");
        }
    }
}