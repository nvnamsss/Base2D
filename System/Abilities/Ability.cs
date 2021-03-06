﻿using System.Collections.Generic;
using System.Collections;
using Seyren.System.Actions;
using Seyren.System.Buffs;
using Seyren.System.Units;
using UnityEngine;
using System;
using static Seyren.System.Units.Unit;
using Seyren.System.Generics;

namespace Seyren.System.Abilities
{
    [Serializable]
    public abstract class Ability
    {
        public static readonly Ability DoNothing = AbilityDoNothing.Instance;
        public static float MaxInterval = 4;
        public static float MinInterval = 1;
        [Obsolete("Ability delegate is obsoleted")]
        public delegate void StatusChangedHandler(Ability sender);
        [Obsolete("Ability delegate is obsoleted")]
        public delegate void CooldownCompletedHandler(Ability sender);
        public event GameEventHandler<Ability> StatusChanged;
        /// <summary>
        /// Trigger when ability cooldown is done and ability is ready to use
        /// </summary>
        public event GameEventHandler<Ability> CooldownCompleted;
        public CastType CastType { get; protected set; }
        public float BaseCoolDown { get; set; }
        /// <summary>
        /// Time between every process for cooldown of an ability <br></br>
        /// </summary>
        public float CooldownInterval { get; set; }
        public float CooldownRemaining
        {
            get
            {
                return _cooldownRemaining;
            }
            set
            {
                float original = _cooldownRemaining;
                _cooldownRemaining = value;

                if (original > 0 && _cooldownRemaining <= 0)
                {
                    CooldownCompleted?.Invoke(this);
                    Debug.Log("cooldown completed");
                }
            }
        }
        public float ManaCost { get; set; }
        /// <summary>
        /// Status of ability
        /// </summary>
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                bool original = _active;
                bool unlock = UnlockCondition();
                if (!unlock)
                {
                    return;
                }

                _active = value;

                if (_active != original)
                {
                    StatusChanged?.Invoke(this);
                }
            }
        }
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }
        [SerializeField]
        public Unit Caster;
        public Unit Target;
        public Vector3 PointTarget;
        protected bool _active;
        protected float _cooldownRemaining;
        [SerializeField]
        protected int _level;
        public virtual bool UnlockAbility()
        {
            Active = true;
            
            return Active;
        }

        public Ability(Unit caster, float cooldown, int level)
        {
            Caster = caster;
            if (cooldown > MaxInterval)
                CooldownInterval = MaxInterval / 10;
            else if (cooldown < MinInterval)
                CooldownInterval = cooldown;
            else
                CooldownInterval = cooldown / 10;
            BaseCoolDown = cooldown;
            CooldownRemaining = 0;
            _level = level;
        }

        public abstract bool Cast();
        /// <summary>
        /// Ability will be cast if condition is true
        /// </summary>
        /// <returns></returns>
        protected abstract bool Condition();
        protected abstract bool UnlockCondition();
    }

}