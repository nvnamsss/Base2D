﻿using Crom.System.UnitSystem;
using Crom.System.BuffSystem;
using Crom.System.BuffSystem.ScriptableObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class TestBuffHPUnit : MonoBehaviour
{
    private Unit unit1;
    private Unit unit2;
    public float seconds;


    private void Start()
    {
        GameObject go1 = new GameObject();
        GameObject go2 = new GameObject();
        go1.AddComponent(typeof(Crom.System.UnitSystem.Unit));
        go2.AddComponent(typeof(Crom.System.UnitSystem.Unit));
        unit1 = go1.GetComponent<Crom.System.UnitSystem.Unit>();
        unit2 = go2.GetComponent<Crom.System.UnitSystem.Unit>();


        //Generate base Value
        ScriptableHPBuff hpBuff = new ScriptableHPBuff();
        hpBuff.HPIncrease = 10;
        unit1.MaxHp = 30;
        unit2.MaxHp = 50;

        //add Component
        TimedHPBuff buff1 = new TimedHPBuff(5f, hpBuff, go1);
        go1.AddComponent(typeof(Crom.System.BuffSystem.BuffableEntity));
        go2.AddComponent(typeof(Crom.System.BuffSystem.BuffableEntity));
        go1.GetComponent<Crom.System.BuffSystem.BuffableEntity>().AddBuff(buff1);
        go1.GetComponent<Crom.System.BuffSystem.BuffableEntity>().AddBuff(new TimedHPBuff(7f, hpBuff, go2));


        Destroy(this, 10f);
    }

    private void Update()
    {
        StartCoroutine(LogBySeconds());
    }




    IEnumerator LogBySeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            Debug.Log(unit1.MaxHp);
            Debug.Log(unit2.MaxHp);
        }
    }

}

