using RPG.AISystems;
using RPG.GameElements.Casters;
using RPG.GameElements.Items;
using RPG.InputSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public class Player : Character
    {
        private BehaviourTreeForCharacter _behaviourTree;
        [SerializeField] private Weapon _bareHandRight;
        [SerializeField] private Weapon _bareHandLeft;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        private AnimatorWrapper _animator;
        private int _equipedTwoHandAnimatorParameter;

        public bool TryEquip(Equipment equipment)
        {
            switch (equipment.bodyPartType)
            {
                case BodyPartType.None:
                    break;
                case BodyPartType.Head:
                    break;
                case BodyPartType.Top:
                    break;
                case BodyPartType.Bottom:
                    break;
                case BodyPartType.Feet:
                    break;
                case BodyPartType.RightHand:
                    break;
                case BodyPartType.LeftHand:
                    break;
                case BodyPartType.TwoHand:
                    {
                        Transform child = null;
                        if (_rightHand.childCount > 0)
                        {
                            child = _rightHand.GetChild(0);
                            child.GetComponent<Equipment>().Unequip(this);
                            Destroy(child.gameObject);
                        }
                        if (_leftHand.childCount > 0)
                        {
                            child = _leftHand.GetChild(0);
                            child.GetComponent<Equipment>().Unequip(this);
                            Destroy(child.gameObject);
                        }

                        Instantiate(equipment, _rightHand).Equip(this);
                        if (equipment is Weapon)
                        {
                            SetAnimatorWeaponParameter(((Weapon)equipment).weaponType);
                        }
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        public bool TryUnequip(Equipment equipment)
        {
            switch (equipment.bodyPartType)
            {
                case BodyPartType.None:
                    break;
                case BodyPartType.Head:
                    break;
                case BodyPartType.Top:
                    break;
                case BodyPartType.Bottom:
                    break;
                case BodyPartType.Feet:
                    break;
                case BodyPartType.RightHand:
                    break;
                case BodyPartType.LeftHand:
                    break;
                case BodyPartType.TwoHand:
                    {
                        Transform child = _rightHand.GetChild(0);
                        if (child.TryGetComponent(out Equipment equiped) && 
                            equiped == equipment)
                        {
                            Destroy(child.gameObject);
                            Instantiate(_bareHandRight, _rightHand);
                            Instantiate(_bareHandLeft, _leftHand);
                            SetAnimatorWeaponParameter(_bareHandRight.weaponType);
                        }
                        else
                        {
                            throw new System.Exception($"[Player] : Tried to unequip wrong equipment");
                        }
                    }
                    break;
                default:
                    break;
            }

            return true;
        }

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<AnimatorWrapper>();
            _equipedTwoHandAnimatorParameter = Animator.StringToHash("equipedTwoHand");
        }

        private void Start()
        {
            AnimatorWrapper animator = GetComponent<AnimatorWrapper>();
            GroundDetector groundDetector = GetComponent<GroundDetector>();
            _behaviourTree = new BehaviourTreeForCharacter(gameObject);
            _behaviourTree.StartBuild()
                .Selector()
                    .Selector()
                        .Condition(() => groundDetector.isDetected == false)
                            .Fall()
                        .Condition(() => groundDetector.isDetected == true)
                            .Move();

            Move move = new Move(_behaviourTree, animator, "doMove");
            Jump jump = new Jump(_behaviourTree, animator, "doJump");
            Attack attack = new Attack(_behaviourTree, animator, "doAttack");
            int jumpParameterID = Animator.StringToHash("doJump");
            int attackParameterID = Animator.StringToHash("doAttack");

            InputManager.instance.RegisterPressAction(KeyCode.Space, () =>
            {
                if (_behaviourTree.currentAnimatorParameterID != jumpParameterID &&
                    groundDetector.TryCastGround(out RaycastHit hit, 0.1f))
                {
                    _behaviourTree.Interrupt(jump);
                }
            });

            InputManager.instance.onMouse0Triggered += () =>
            {
                if (_behaviourTree.currentAnimatorParameterID != attackParameterID)
                {
                    _behaviourTree.Interrupt(attack);
                }
            };


            move.Invoke();
            _behaviourTree.currentAnimatorParameterID = Animator.StringToHash("doMove");
        }

        private void Update()
        {
            _behaviourTree.Run();
        }

        private void SetAnimatorWeaponParameter(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.None:
                    break;
                case WeaponType.BareHand:
                    {
                        _animator.SetBool(_equipedTwoHandAnimatorParameter, false);
                    }
                    break;
                case WeaponType.Sword2Handed:
                    {
                        _animator.SetBool(_equipedTwoHandAnimatorParameter, true); 
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void StartRightHandCasting()
        {
            base.StartRightHandCasting();
            if (_rightHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.doCast = true;
            }
        }

        protected override void FinishRightHandCasting()
        {
            base.FinishRightHandCasting();
            if (_rightHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.doCast = false;
            }
        }

        protected override void StartLeftHandCasting()
        {
            base.StartRightHandCasting();
            if (_leftHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.doCast = true;
            }
        }

        protected override void FinishLeftHandCasting()
        {
            base.FinishRightHandCasting();
            if (_leftHand.GetChild(0).TryGetComponent(out Weapon weapon))
            {
                weapon.doCast = false;
            }
        }
    }
}