using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float 		Accel		= 0.025f;
	public float 		MaxSpeed	= 2.0f;
	public float 		Handling 	= 50.0f;
	public float 		JumpForce 	= 10.0f;

	private float 		m_brake = 0.1f;
	private bool		m_isJump = false;
	private Rigidbody 	m_rigidBody;
	private Animator	m_animator;


	float m_Accell = 0;

	// Use this for initialization
	void Start () {
		m_rigidBody = transform.GetComponent<Rigidbody> ();
		m_animator 	= gameObject.GetComponent<Animator> ();

		m_brake = MaxSpeed / 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

		// 前後移動
		Move ();

		// 回転
		Rotation ();

		// Jump
		Jump();
	}


	/// <summary>
	/// 前後移動.
	/// </summary>
	private void Move ()
	{
		// 前後移動
		if (Input.GetKey (KeyCode.UpArrow))
		{
			m_Accell += Accel;
			if (m_Accell > 1.0f)
			{
				m_Accell = 1.0f;
			}
		}
		else if (Input.GetKey (KeyCode.DownArrow))
		{
			m_Accell -= Accel;
			if (m_Accell < -1.0f)
			{
				m_Accell = -1.0f;
			}
		}
		else
		{

			if (m_Accell > 0)
			{
				m_Accell -= m_brake;
			}
			if (m_Accell < 0)
			{
				m_Accell += m_brake;
			}

			if (Mathf.Abs (m_Accell) <= m_brake)
			{
				m_Accell = 0f;
			}

		}

		float speedMatrix = 1.0f;
		float speed = m_Accell * MaxSpeed * speedMatrix;
		transform.position += transform.TransformDirection (Vector3.forward) * speed * Time.deltaTime;

		if (m_animator)
		{
			// アニメーション速度
			if (m_Accell > 0)
			{
				m_animator.speed = 0.5f + (m_Accell * 0.5f);
			} else
			{
				m_animator.speed = 1.0f;
			}

			// Animatorのパラメータに値をセット
			m_animator.SetFloat ("Move", Mathf.Abs(speed));
		}

	}


	/// <summary>
	/// 回転.
	/// </summary>
	private void Rotation()
	{
		// 回転
		Vector3 rot = transform.rotation.eulerAngles;
		float handle = 0f;
		if (Input.GetKey (KeyCode.RightArrow))
		{
			handle = Handling * Time.deltaTime;
			rot.y += handle;
		}
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			handle = Handling * Time.deltaTime;
			rot.y -= handle;
		}
		transform.rotation = Quaternion.Euler (rot);

	}


	private void Jump()
	{
		// ジャンプ
		if (Input.GetKey (KeyCode.Space))
		{
			if (!m_isJump)
			{
				m_rigidBody.AddForce (Vector3.up * JumpForce, ForceMode.Impulse);
			}
			m_isJump = true;
		}
		else
		{
			m_isJump = false;
		}
	}
}
