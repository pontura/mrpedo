using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] private GrabbableSO prefab;
        [SerializeField] private int initialSize = 80;
        [SerializeField] private bool canExpand = true;

        private Queue<GrabbableSO> poolGrabQueue = new Queue<GrabbableSO>();

        void Awake()
        {
            InitializePool();
        }

        void InitializePool()
        {
            for (int i = 0; i < initialSize; i++)
            {
                CreateGrabbableObject();
            }
        }

        GrabbableSO CreateGrabbableObject()
        {
            GrabbableSO obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            poolGrabQueue.Enqueue(obj);
            return obj;
        }

        public GrabbableSO GetGrabbable()
        {
            if (poolGrabQueue.Count == 0)
            {
                if (canExpand)
                    CreateGrabbableObject();
                else
                    return null;
            }

            GrabbableSO obj = poolGrabQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnGrabbable(GrabbableSO obj)
        {
            obj.gameObject.SetActive(false);
            poolGrabQueue.Enqueue(obj);
        }
    }

}
