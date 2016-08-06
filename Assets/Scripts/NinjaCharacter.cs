using UnityEngine;

namespace Scripts {
	[RequireComponent(typeof(Animator))]
	public class NinjaCharacter : MonoBehaviour {
		[SerializeField] float m_AnimSpeedMultiplier = 1f;

		Animator m_Animator;
		float m_TurnAmount;
		float m_ForwardAmount;
		public Transform worldObject;
		public Transform ninjaHolder;
		public float worldSpeed = 1f;
		public float worldTurnRate = 1f;
		public float turnBoost = 22;


		// Use this for initialization
		void Start () {
			m_Animator = GetComponent<Animator>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Move(Vector2 move, bool jump)
		{
			bool jumping = false;
			//if (move.magnitude > 1f) move.Normalize();
			m_TurnAmount = move.y;
			m_ForwardAmount = move.x;
			print (m_ForwardAmount);
			if (jump && m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("Running")) {
				jumping = jump;
			}

			worldObject.transform.position = worldObject.transform.position - Vector3.forward * Time.deltaTime * worldSpeed * m_ForwardAmount;
			if (m_TurnAmount != 0f) {
				ninjaHolder.Rotate (new Vector3 (0, 0, m_TurnAmount * m_ForwardAmount * Time.deltaTime * worldTurnRate));
			}
			transform.localRotation = Quaternion.AngleAxis (m_TurnAmount * turnBoost, Vector3.up);
			// send input and other state parameters to the animator
			UpdateAnimator(move, jumping);
		}

		void UpdateAnimator(Vector2 move, bool jumping)
		{
			m_Animator.SetFloat("Speed", m_ForwardAmount);
			m_Animator.SetFloat("Direction", m_TurnAmount);
			m_Animator.SetBool ("Jump", jumping);
			print (m_ForwardAmount);

			if (!jumping)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
				// don't use that while airborne
				m_Animator.speed = 1;
			}
		}			
	}
}
