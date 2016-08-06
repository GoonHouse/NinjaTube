﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Scripts {
	[RequireComponent(typeof (NinjaCharacter))] 
	public class InputHandler : MonoBehaviour {

		private NinjaCharacter m_Character; // A reference to the NinjaCharacter on the object
		private Vector2 m_Move;             // Movement controls
		private bool m_Jump;

		// Use this for initialization
		private void Start () {
			// get the Ninja character ( this should never be null due to require component )
			m_Character = GetComponent<NinjaCharacter>();
		}
		
		// Update is called once per frame
		private void Update () {
			if (!m_Jump) {
				m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}
		}

		// Fixed update is called in sync with physics
		private void FixedUpdate()
		{
			// read inputs
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			//bool crouch = Input.GetKey(KeyCode.C);


			m_Move = new Vector2 (v,h);


			// pass all parameters to the character control script
			m_Character.Move(m_Move, m_Jump);
			m_Jump = false;
		}
	}
}