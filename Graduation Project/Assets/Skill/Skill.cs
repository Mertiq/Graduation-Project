﻿using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 1)]
public class Skill : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;
    [SerializeField] private int currentValue;

    public string SkillName
    {
        get => skillName;
        set => skillName = value;
    }

    public int MinValue
    {
        get => minValue;
        set => minValue = value;
    }

    public int MaxValue
    {
        get => maxValue;
        set => maxValue = value;
    }

    public int CurrentValue
    {
        get => currentValue;
        set => currentValue = value;
    }
}
