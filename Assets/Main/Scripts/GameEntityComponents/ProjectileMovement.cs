using UnityEngine;

namespace Lesson_4_7.GameEntityComponents
{
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _damage;
        [SerializeField]
        private float _lifeTime;

        public string ParentName { get; private set; }
        public float Damage => _damage;

        private void Start()
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.velocity = transform.up * _speed;
            Destroy(gameObject, _lifeTime);
        }

        public void SetData(string parentName)
        {
            ParentName = parentName;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
