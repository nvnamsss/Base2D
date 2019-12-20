﻿using System.Collections;
using System.Collections.Generic;
using Base2D.System.DamageSystem;
using Base2D.System.UnitSystem.Units;
using UnityEngine;

namespace Base2D.System.UnitSystem.Projectiles
{
    public partial class Projectile : MonoBehaviour, IAttribute
    {
        public Projectile()
        {
            HitDelay = 10;
            Speed = 0.01f;
            Angle = 90;
            _hitDelay = 0;
            TimeExpire = float.MaxValue;
            IsPenetrate = false;
            ProjectileType = ProjectileType.None;
        }
        public virtual void Hit()
        {
            animator?.SetBool("hit", true);
            if (_hitDelay > 0)
            {
                return;
            }

            if (_hit >= MaxHit)
            {
                Destroy(gameObject);               
            }

            _hit = _hit + 1;
            _hitDelay = HitDelay;

            if (!IsPenetrate)
            {
                Collider.isTrigger = true;
            }
            else
            {

            }

            OnHit?.Invoke(this);
        }

        public virtual void Move()
        {
            animator?.SetBool("move", true);
            float rad = (float)(Angle * Mathf.Deg2Rad);
            Vector2 velocity = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)) * (float)Speed;
        }

        /// <summary>
        /// Remove projectile
        /// </summary>
        public virtual void Remove()
        {

        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            Body = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            TimeExpired += (sender) =>
            {
                Destroy(sender.gameObject);
            };
        }

        // Update is called once per frame
        protected virtual void FixedUpdate()
        {
            _hitDelay -= Time.deltaTime;
            TimeExpire -= Time.deltaTime;
            Move();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            Hit();
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            Hit();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Hit();
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            Hit();
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("[Projectile] - Exit");

        }

        protected static GameObject CreateObject(string name, Vector3 location, Quaternion rotation, Sprite sprite, RuntimeAnimatorController controller)
        {
            GameObject go = new GameObject(name);
            SpriteRenderer render = go.AddComponent<SpriteRenderer>();
            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = controller;
            go.AddComponent<Rigidbody2D>();
            go.AddComponent<BoxCollider2D>();
            go.transform.position = location;
            go.transform.rotation = rotation;
            render.sprite = sprite;
            return go;
        }
    }

}
