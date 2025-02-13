using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Game/Entity")]
public class EntityObject : ScriptableObject
{
    [SerializeField] private float _hp = 0f;
    [SerializeField] private float _def = 0f;
    [SerializeField] private float _atk = 0f;

    public float HP
    {
        get => _hp; 
        set => _hp = value;
    }

    public float DEF
    {
        get => _def;
        set => _def = value;
    }

    public float ATK
    {
        get => _atk;
        set => _atk = value;
    }
}