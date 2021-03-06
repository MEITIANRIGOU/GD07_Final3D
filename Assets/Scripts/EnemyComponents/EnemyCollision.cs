using System;
using EventClass;
using Systems;
using UnityEngine;

namespace EnemyComponents
{
    public class EnemyCollision : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(transform.parent.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BasicControl>())
            {
                NewEventSystem.Instance.Publish(new GameEndEvent(true));
            }
        }
    }
}