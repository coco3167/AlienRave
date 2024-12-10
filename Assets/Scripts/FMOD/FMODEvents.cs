using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    [Header("GreenPlayer")]
    [SerializeField] public EventReference greenPlayerAttack { get; private set; }
    [SerializeField] public EventReference greenPlayerIsHurt { get; private set; }
    
    [Header("PinkPlayer")]
    [SerializeField] public EventReference pinkPlayerAttack { get; private set; }
    [SerializeField] public EventReference pinkPlayerIsHurt { get; private set; }
    
    [Header("DrunkEnemy")]
    [SerializeField] public EventReference greenDrunkEnemySeesPlayer { get; private set; }
    [SerializeField] public EventReference greenDrunkEnemyIsHurt { get; private set; }
    [SerializeField] public EventReference pinkDrunkEnemySeesPlayer { get; private set; }
    [SerializeField] public EventReference pinkDrunkEnemyIsHurt { get; private set; }

    [Header("KisserEnemy")] 
    [SerializeField] public EventReference kisserEnemyKiss { get; private set; }
    [SerializeField] public EventReference kisserEnemyDeath { get; private set; }
    [SerializeField] public EventReference greenKisserEnemyIsHurt { get; private set; }
    [SerializeField] public EventReference pinkKisserEnemyIsHurt { get; private set; }

    [Header("HybridEnemy")] 
    [SerializeField] public EventReference hybridEnemyBurp { get; private set; }
    [SerializeField] public EventReference greenHybridEnemyIsHurt { get; private set; }
    [SerializeField] public EventReference pinkHybridEnemyIsHurt { get; private set; }
    [SerializeField] public EventReference hybridEnemyDeath { get; private set; }
    
    [Header("Amb")]
    [SerializeField] public EventReference crowd2D { get; private set; }
    [SerializeField] public EventReference crowdChenille { get; private set; }
    
    [Header("Music")]
    [SerializeField] public EventReference music { get; private set; }
    
    public static FMODEvents instance { get; private set;}
    private void Awake()
    {
        instance = this;
    }
    
    






}
