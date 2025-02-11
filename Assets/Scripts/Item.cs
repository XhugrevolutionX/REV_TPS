using System;
using UnityEngine;

public class Item : MonoBehaviour
{
   public event Action<Item> OnPicked;

   void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         OnPicked?.Invoke(this);
         Destroy(gameObject);
      }
   }
}
