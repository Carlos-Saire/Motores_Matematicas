using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewColorData", menuName = "Color Data")]

public class NewMaterial : ScriptableObject
{
    [SerializeField] private Material[] material;
    public Material GetRandonMaterial()
    {
        return material[Random.Range(0, material.Length)];
    }
}
