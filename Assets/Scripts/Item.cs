using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemValues[] values;
    public ItemValues myItemValue;
    
    public event Action<Item> OnPicked;

    void Start()
    {
        myItemValue = values[Random.Range(0, values.Length)];
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(myItemValue.itemName);
        
        if (other.CompareTag("Player"))
        {
            OnPicked?.Invoke(this);
            Destroy(gameObject);
        }
    }
}