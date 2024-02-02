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
    
    //ひるみの変数
    private bool isFlinching = false;
    private float flinchDuration = 0.5f;
    private float flinchTimer = 0f;

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
        //怯み中はなにもしない
        if (isFlinching)
        {
            flinchTimer -= Time.deltaTime;
            if(flinchTimer <= 0f)
            {
                isFlinching = false;
            }
            return;
            
            
        }
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
        if (!isFlinching)
        {
            isFlinching = true;
            flinchTimer = flinchDuration;
            //ひるみのアニメーションなど
            animator.SetTrigger("Damage");
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

    public void Win()
    {
        if (!enemy)
        {
            gameWinCanvas.SetActive(true);
        }
    }
}