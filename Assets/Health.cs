﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //public int PlayerHealthQuantity = 5;
    //public int SpawnerHealthQuantity = 20;
    //public int BarrelHealthQuantity = 2;


    // delegate signature de fonction
    public delegate void HealthEvent(Health health);

    // Listeners
    public HealthEvent OnChanged;
    public HealthEvent OnHit;
    public HealthEvent OnDeath;

    public int Max;

    private int _value;
    public float InvincibilityTime = 0.1f;
    public float InvincibilityTimer { get; private set; }

    public int Value
    {
        get { return _value; }
        set
        {
            var previous = _value;

            _value = Mathf.Clamp(value, 0, Max);

            if (_value != previous)
            {
                OnChanged?.Invoke(this);

                if (_value < previous)
                {
                    InvincibilityTimer = InvincibilityTime;
                    OnHit?.Invoke(this);
                }

                if (_value <= 0)
                    OnDeath?.Invoke(this);
            }
        }
    }

    private void Awake()
    {
        Value = Max;
    }

    public bool CanBeDamaged
    {
        get
        {
            return InvincibilityTimer <= 0.0f;
        }
    }

    private void Update()
    {
        InvincibilityTimer -= Time.deltaTime;
    }
}
