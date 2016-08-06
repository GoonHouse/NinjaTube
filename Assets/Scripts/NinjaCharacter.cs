using UnityEngine;

namespace Scripts {
	[RequireComponent(typeof(Animator))]
	public class NinjaCharacter : MonoBehaviour {
		[SerializeField] float m_AnimSpeedMultiplier = 1f;

		Animator m_Animator;
		float m_TurnAmount;
		float m_ForwardAmount;

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

			// send input and other state parameters to the animator
			UpdateAnimator(move, jumping);
		}

		void UpdateAnimator(Vector2 move, bool jumping)
		{
			m_Animator.SetFloat("Speed", m_ForwardAmount);
			m_Animator.SetFloat("Direction", m_TurnAmount);
			m_Animator.SetBool ("Jump", jumping);
			print (m_ForwardAmount);
			m_Animator.speed = m_AnimSpeedMultiplier;
		}			
	}
}
