﻿using Base2D.System.UnitSystem.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Base2D.System.AbilitySystem
{
    public abstract class ActiveAbility : Ability
    {
        public delegate void CastingSpellHandler(ActiveAbility sender, CastingSpellEventArgs e);
        public delegate void CastedSpellHandler(ActiveAbility sender);
        public event CastingSpellHandler Casting;
        public event CastedSpellHandler Casted;
        public float BaseCastTime { get; set; }
        public float CastTimeRemaining { get; set; }
        public bool IsCasting { get; set; }
        public BreakType BreakType { get; set; }
        public float CastInterval { get; set; }
        protected Coroutine castCoroutine;
        protected Coroutine cooldownCoroutine;
        protected abstract void DoCastAbility();
        public ActiveAbility(Unit caster, float castTime, float cooldown, int level) :
            base(caster, cooldown, level)
        {
            CastType = CastType.Active;
            BaseCastTime = castTime;
            CastInterval = castTime;
        }

        public override bool Cast()
        {
            if (!Condition())
            {
                return false;
            }

            CastingSpellEventArgs cing = new CastingSpellEventArgs();
            Casting?.Invoke(this, cing);

            if (cing.Cancel)
            {
                return false;
            }

            castCoroutine = Caster.StartCoroutine(CastingProcess(CastInterval, BaseCastTime));
            return true;
        }

        protected virtual IEnumerator CastingProcess(float delayTime, float castTime)
        {
            var wait = new WaitForSeconds(delayTime);
            IsCasting = true;
            CastTimeRemaining = castTime;
            Debug.Log("Start " + Time.timeScale);

            while (CastTimeRemaining > 0)
            {
                yield return wait;
                CastTimeRemaining -= delayTime;
            }

            if (IsCasting)
            {
                Debug.Log("Done");
                IsCasting = false;
                DoCastAbility();
                cooldownCoroutine = Caster.StartCoroutine(CastedProcess(CooldownInterval, BaseCoolDown));
            }
        }

        protected virtual IEnumerator CastedProcess(float delayTime, float cooldownTime)
        {
            var wait = new WaitForSeconds(delayTime);
            Casted?.Invoke(this);
            Active = false;
            IsCasting = false;
            CooldownRemaining = cooldownTime - delayTime;

            while (CooldownRemaining >= 0)
            {
                yield return wait;
                CooldownRemaining -= delayTime;
            }
            Active = true;
        }
    }
}
