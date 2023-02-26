using UnityEngine;


[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    [HideInInspector]
    public Pawn pawn;

    protected virtual void Start() { }
    protected virtual void Awake() { }
    protected virtual void Update(){ }

    
   
}
