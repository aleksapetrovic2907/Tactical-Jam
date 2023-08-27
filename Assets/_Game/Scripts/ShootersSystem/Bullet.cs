using UnityEngine;

namespace Aezakmi.ShootersSystem
{
    public class Bullet : MonoBehaviour
    {
        public float speed;

        private const float DESTROY_DELAY_UPON_IMPACT = 2f;
        private Rigidbody m_rigidBody;

        public void SetData(Vector3 forward)
        {
            m_rigidBody = GetComponent<Rigidbody>();
            m_rigidBody.velocity = forward * speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var shootable = collision.collider.GetComponent<IShootable>();
            if (shootable == null) return;
            shootable.GetShot(this);
            m_rigidBody.useGravity = true;
            Destroy(gameObject, DESTROY_DELAY_UPON_IMPACT);
        }
    }
}