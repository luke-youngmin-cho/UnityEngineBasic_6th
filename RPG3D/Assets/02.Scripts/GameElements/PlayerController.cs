using RPG.AISystems;
using RPG.Controllers;
using RPG.DataModels;
using RPG.Datum;
using RPG.GameElements.Casters;
using RPG.GameElements.Items;
using RPG.InputSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
    public class PlayerController : Character, IController
    {
        public BehaviourTreeForCharacter behaviourTree;
        [SerializeField] private Weapon _bareHandRight;
        [SerializeField] private Weapon _bareHandLeft;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        private AnimatorWrapper _animator;
        private int _equipedTwoHandAnimatorParameter;

        public bool controllable
        {
            get => _controllable;
            set
            {
                _controllable = value;
                Cursor.visible = value == false;
                Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.Confined;
                ControllerManager.instance.Get<CameraController>().controllable = value;
            }
        }
        private bool _controllable;

        public Equipment GetEquipment(BodyPartType bodyPartType)
        {
            switch (bodyPartType)
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
                    {
                        return _rightHand.GetChild(0).GetComponent<Equipment>();
                    }
                case BodyPartType.LeftHand:
                    {
                        return _leftHand.GetChild(0).GetComponent<Equipment>();
                    }
                case BodyPartType.TwoHand:
                    {
                        return _rightHand.GetChild(0).GetComponent<Equipment>();
                    }
                default:
                    break;
            }

            return null;
        }

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
                    {
                        Transform child = null;
                        if (_rightHand.childCount > 0)
                        {
                            child = _rightHand.GetChild(0);
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
                case BodyPartType.LeftHand:
                    {
                        Transform child = null;
                        if (_leftHand.childCount > 0)
                        {
                            child = _leftHand.GetChild(0);
                            child.GetComponent<Equipment>().Unequip(this);
                            Destroy(child.gameObject);
                        }

                        Instantiate(equipment, _leftHand).Equip(this);
                        if (equipment is Weapon)
                        {
                            SetAnimatorWeaponParameter(((Weapon)equipment).weaponType);
                        }
                    }
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

        public bool TryUnequip(BodyPartType bodyPartType)
        {
            switch (bodyPartType)
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
                        if (child.TryGetComponent(out Equipment equiped))
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
            ControllerManager.instance.Register(this);
            ControllerManager.instance.Authorize(this);
            TryEquip(_bareHandRight);
            TryEquip(_bareHandLeft);

            ItemsEquippedDataModel itemsEquippedDataModel = DataModelManager.instance.Get<ItemsEquippedDataModel>();
            foreach (var itemID in itemsEquippedDataModel)
            {
                if (itemID > 0)
                    TryEquip((Equipment)ItemInfoAssets.instance[itemID].prefab);
            }
            itemsEquippedDataModel.itemChanged += ((slotID, itemID) =>
            {
                if (itemID > 0)
                    TryEquip((Equipment)ItemInfoAssets.instance[itemID].prefab);
                else
                    TryUnequip((BodyPartType)slotID);
            });


            AnimatorWrapper animator = GetComponent<AnimatorWrapper>();
            GroundDetector groundDetector = GetComponent<GroundDetector>();
            behaviourTree = new BehaviourTreeForCharacter(gameObject);
            behaviourTree.StartBuild()
                .Selector()
                    .Condition(() => GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).loop == false ? 
                                     animator.GetNormalizedTime(0) > 0.9f : true)
                    .Selector()
                        .Condition(() => groundDetector.isDetected == false)
                            .Fall()
                        .Condition(() => groundDetector.isDetected == true)
                            .Move();

            Move move = new Move(behaviourTree, animator, "doMove");
            Jump jump = new Jump(behaviourTree, animator, "doJump");
            Attack attack = new Attack(behaviourTree, animator, "doAttack");
            int moveParameterID = Animator.StringToHash("doMove");
            int jumpParameterID = Animator.StringToHash("doJump");
            int attackParameterID = Animator.StringToHash("doAttack");

            InputManager.instance.RegisterPressAction(KeyCode.Space, () =>
            {
                if (controllable == false)
                    return;

                if (behaviourTree.currentAnimatorParameterID != jumpParameterID &&
                    groundDetector.TryCastGround(out RaycastHit hit, 0.1f))
                {
                    behaviourTree.Interrupt(jump);
                }
            });

            InputManager.instance.onMouse0Triggered += () =>
            {
                if (controllable == false)
                    return;

                if (behaviourTree.currentAnimatorParameterID != attackParameterID)
                {
                    behaviourTree.Interrupt(attack);
                }
            };

            behaviourTree.Interrupt(move);
            behaviourTree.currentAnimatorParameterID = Animator.StringToHash("doMove");

            LayerMask npcMask = 1 << LayerMask.NameToLayer("NPC");
            LayerMask itemMask = 1 << LayerMask.NameToLayer("ItemController");
            InputManager.instance.RegisterDownAction(KeyCode.F, () =>
            {
                if (controllable == false)
                    return;

                Collider[] cols;
                cols = Physics.OverlapSphere(transform.position, 2.0f, npcMask);
                if (cols.Length > 0)
                {
                    NPC npc;
                    for (int i = 0; i < cols.Length; i++)
                    {
                        npc = cols[i].GetComponent<NPC>();
                        if (npc.canInteract)
                        {
                            npc.StartInteraction(gameObject);
                            return;
                        }
                    }
                }

                cols = Physics.OverlapSphere(transform.position, 2.0f, itemMask);
                if (cols.Length > 0)
                {
                    cols[0].GetComponent<ItemController>().PickUp(transform);
                }
            });
        }

        private void Update()
        {
            behaviourTree.Run();
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