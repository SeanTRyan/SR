using Actions;
using Broadcasts;
using Controls;
using Managers;
using Movements;
using Survival;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Controller class for the character.
    /// </summary>
    public class CharacterManager : MonoBehaviour, IBroadcast
    {
        [SerializeField] private PlayerNumber m_PlayerNumber;

        private BroadcastMessage m_Message;

        //All of the different character components 
        private Jump m_Jump;
        private Movement m_Movement;
        private CharacterAttack m_Attack;
        private Health m_Health;
        private Shield m_Shield;
        private Evasion m_Evasion;
        private Gravity m_Gravity;

        #region Controls
        //Device for controlling the player
        private Device m_Device = null;

        //Moves the character based on joystick input
        private Vector2 m_Move;

        [Header("Jump Inputs")]
        //Range for determining whether the character wants to fall fast
        [SerializeField] private ActionRange m_FallRange = new ActionRange();

        //How long the jump button can be held
        [SerializeField] private ActionHold m_JumpAction = new ActionHold();

        private int neutralAttack = 1;
        private int forwardAttack = 2;
        private int backAttack = 3;
        private int downAttack = 4;
        private int upAttack = 5;
        #endregion

        //Stops all updates that belong to this object
        public bool Stop { get; set; }

        private void Awake()
        {
            m_Jump = GetComponent<Jump>();
            m_Movement = GetComponent<Movement>();
            m_Attack = GetComponent<CharacterAttack>();
            m_Evasion = GetComponent<Evasion>();
            m_Health = GetComponent<Health>();
            m_Shield = GetComponent<Shield>();
            m_Gravity = GetComponent<Gravity>();
        }

        public void InitialiseDevice(int playerNumber)
        {
            if (playerNumber >= Input.GetJoystickNames().Length)
                return;

            m_Device = InputManager.GetDevice(playerNumber);
        }

        private void Update()
        {
            if (m_Message == Broadcasts.BroadcastMessage.Stunned ||
                m_Message == Broadcasts.BroadcastMessage.Dead)
                return;

            if (Stop || m_Device == null)
                return;

            ExecuteDevice();

            ExecuteJump();

            ExecuteAttack();

            ExecuteShield();
        }

        private void ExecuteDevice()
        {
            float horizontal = m_Device.LeftHorizontal.Value;
            float vertical = m_Device.LeftVertical.Value;

            m_Move = new Vector2(horizontal, vertical);
        }

        private void FixedUpdate()
        {
            ExecuteGravity();

            if (m_Message == Broadcasts.BroadcastMessage.Stunned ||
                m_Message == Broadcasts.BroadcastMessage.Dead)
                return;

            if (!m_Evasion.Rolling && !m_Evasion.Dodging && !m_Attack.IsAttacking)
                ExecuteMove();

            ExecuteEvade();
        }

        private void ExecuteJump()
        {
            if (!m_Jump)
                return;

            bool jump = m_Device.L1.Press;
            bool jumpHold = m_Device.L1.Hold;

            m_Jump.Execute(m_Move, jump, jumpHold);

            m_Jump.Fall(transform.position.y);
        }

        private void ExecuteMove()
        {
            if (m_Movement == null)
                return;

            m_Movement.Move(m_Move);
        }

        private void ExecuteAttack()
        {
            if (!m_Attack)
                return;

            int attackID = 0;
            GetAttackID(ref attackID);

            m_Attack.Execute(attackID);
        }

        private void GetAttackID(ref int attackID)
        {
            float forward = transform.forward.x;

            attackID = (m_Device.Action1.Press) ? neutralAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftHorizontal.Value * forward > 0.1f) ? forwardAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftHorizontal.Value * forward < -0.1f) ? backAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftVertical.Value < -0.1) ? downAttack : attackID;
            attackID = (m_Device.Action1.Press && m_Device.LeftVertical.Value > 0.1) ? upAttack : attackID;
        }

        private void ExecuteEvade()
        {
            if (!m_Evasion || !m_Shield)
                return;

            m_Evasion.Execute(m_Move, m_Shield.Shielding);
        }

        private void ExecuteShield()
        {
            if (!m_Shield)
                return;

            bool shield = m_Device.R1.Hold;

            m_Shield.Execute(m_Device.R1.Hold);
        }

        private void ExecuteGravity()
        {
            if (!m_Gravity)
                return;

            m_Gravity.Execute();
        }

        public void Inform(BroadcastMessage message)
        {
            m_Message = message;
        }
    }
}