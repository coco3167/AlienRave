using System;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    [field : Header("GreenPlayer")]
    [field : SerializeField] public EventReference greenPlayerAttack { get; private set; }
    [field : SerializeField] public EventReference greenPlayerIsHurt { get; private set; }
    
    [field : Header("PinkPlayer")]
    [field : SerializeField] public EventReference pinkPlayerAttack { get; private set; }
    [field : SerializeField] public EventReference pinkPlayerIsHurt { get; private set; }
    
    [field : Header("DrunkEnemy")]
    [field : SerializeField] public EventReference greenDrunkEnemySeesPlayer { get; private set; }
    [field : SerializeField] public EventReference greenDrunkEnemyIsHurt { get; private set; }
    [field : SerializeField] public EventReference pinkDrunkEnemySeesPlayer { get; private set; }
    [field : SerializeField] public EventReference pinkDrunkEnemyIsHurt { get; private set; }
    [field : SerializeField] public EventReference drunkEnemyChenille { get; private set; }

    [field : Header("KisserEnemy")] 
    
    [field : SerializeField] public EventReference kisserEnemyDeath { get; private set; }
    [field : SerializeField] public EventReference greenKisserEnemyIsHurt { get; private set; }
    [field : SerializeField] public EventReference pinkKisserEnemyIsHurt { get; private set; }

    [field : Header("HybridEnemy")] 
    [field : SerializeField] public EventReference hybridEnemyBurp { get; private set; }
    [field : SerializeField] public EventReference greenHybridEnemyIsHurt { get; private set; }
    [field : SerializeField] public EventReference pinkHybridEnemyIsHurt { get; private set; }
    [field : SerializeField] public EventReference hybridEnemyDeath { get; private set; }
    
    [field : Header("Projectiles")]
    [field : SerializeField] public EventReference projectileObstacle { get; private set; }
    [field : SerializeField] public EventReference kisserEnemyKiss { get; private set; }
    
    [field : Header("UI")]
    [field : SerializeField] public EventReference uIOnButton { get; private set; }
    [field : SerializeField] public EventReference uIClickButton { get; private set; }
    
    [field : Header("Amb")]
    [field : SerializeField] public EventReference crowd2D { get; private set; }
    [field : SerializeField] public EventReference crowdGroup { get; private set; }
    
    [field : Header("Music")]
    [field : SerializeField] public EventReference music { get; private set; }
    
    [field : Header("Parameters")]
    [field : SerializeField] private string musicStateParameterName = "LevelState";
    [field : SerializeField] private string pauseParameterName = "GameStatus";
    
    public static FMODEvents Instance { get; private set;}
    private void Awake()
    {
        Instance = this;
    }
    
}
