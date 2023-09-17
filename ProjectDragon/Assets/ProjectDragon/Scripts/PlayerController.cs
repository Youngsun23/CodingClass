using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dragon
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }


        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position + Vector3.up, transform.forward + Vector3.up);
            //Gizmos.DrawWireSphere(transform.position, 10f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }

        public float moveSpeed;
        public float rotationSpeed;
        public float currentHP;
        public float maxHP;

        public float raycastRange;
        public int raycastAccuracy = 1;
        public float detectionRadius;
        public List<GameObject> detectedObjectList = new List<GameObject>();

        private float currentRotate = 0;
        private float buffedSpeedRate = 1;
        private TriggerGate currentGate;


        public Transform firePosition;
        public Bullet bulletPrefab;
        public float fireRate;
        public float angleSpeed;

        public int maxAmmo;
        public int curAmmo;

        private float fireAngle;
        private float lastFireTime;
        public int iterationStep = 30;


        public event Action<float, float> OnChangedHP;
        public event Action<int, int> OnChangedAmmo;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            Update_Movement();
            Update_FireControl();

            detectedObjectList.Clear();

            #region #1. Raycast & DrawLine
            //Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
            //Debug.DrawLine(ray.origin, ray.origin + (ray.direction) * raycastRange, Color.red);
            //if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
            //{
            //    Debug.Log($"Front Object Name : {hitInfo.collider.name}", hitInfo.transform.gameObject);
            //}
            #endregion

            #region #2. 360' Raycast
            //if (raycastAccuracy <= 0)
            //{
            //    raycastAccuracy = 1;
            //}

            //for (int i = 0; i < 360; i += raycastAccuracy)
            //{
            //    Quaternion rotation = Quaternion.Euler(0, i, 0);
            //    Vector3 direction = rotation * transform.forward;

            //    Ray circleRay = new Ray(transform.position + Vector3.up, direction);
            //    Debug.DrawLine(circleRay.origin, circleRay.origin + (circleRay.direction) * raycastRange, Color.blue);

            //    if (Physics.Raycast(circleRay, out RaycastHit hitInfo, raycastRange))
            //    {
            //        if (false == detectedObjectList.Contains(hitInfo.transform.gameObject))
            //        {
            //            detectedObjectList.Add(hitInfo.transform.gameObject);
            //        }
            //    }
            //}
            #endregion

            #region #3. Physics.Overlap Example
            //Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius);
            //for (int i = 0; i < detectedColliders.Length; i++)
            //{
            //    detectedObjectList.Add(detectedColliders[i].gameObject);

            //}
            #endregion

            #region 포물선 운동 궤적 그려보기 #1
            //Vector3 previousPoint = firePosition.position;
            //Vector3 nextPoint = firePosition.position;
            //for (int i = 1; i <= iterationStep; i++)
            //{
            //    float simulationTime = i / (float)iterationStep;
            //    nextPoint += 
            //        (bulletPrefab.speed * firePosition.forward) + 
            //        (Vector3.up * bulletPrefab.gravity * simulationTime * simulationTime / 2f);
            //    Debug.DrawLine(previousPoint, nextPoint);
            //    previousPoint = nextPoint;
            //}
            #endregion


        }

        private void Update_FireControl()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (Time.time > lastFireTime + fireRate)
                {
                    if (curAmmo > 0)
                    {
                        var newBullet = Instantiate(bulletPrefab);
                        newBullet.transform.position = firePosition.position;
                        newBullet.transform.rotation = firePosition.rotation;
                        newBullet.gameObject.SetActive(true);

                        lastFireTime = Time.time;
                        curAmmo--;
                        OnChangedAmmo?.Invoke(curAmmo, maxAmmo);

                        if (curAmmo <= 0)
                        {
                            StartCoroutine(DelayedReload());
                            IEnumerator DelayedReload()
                            {
                                yield return new WaitForSeconds(3f);

                                curAmmo = maxAmmo;
                                OnChangedAmmo?.Invoke(curAmmo, maxAmmo);
                            }
                        }
                    }
                }
            }

            if (Input.GetKey(KeyCode.R))
            {
                fireAngle -= angleSpeed * Time.deltaTime;
                firePosition.localRotation = Quaternion.Euler(fireAngle, 0, 0);
            }

            if (Input.GetKey(KeyCode.F))
            {
                fireAngle += angleSpeed * Time.deltaTime;
                firePosition.localRotation = Quaternion.Euler(fireAngle, 0, 0);
            }
        }

        private void Update_Movement()
        {
            Vector3 movement = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                movement.z += 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                movement.z -= 1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                movement.x -= 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movement.x += 1;
            }

            transform.Translate(movement * moveSpeed * buffedSpeedRate * Time.deltaTime, Space.Self);

            if (Input.GetKey(KeyCode.Q))
            {
                currentRotate -= rotationSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.E))
            {
                currentRotate += rotationSpeed * Time.deltaTime;
            }

            transform.rotation = Quaternion.Euler(0, currentRotate, 0);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (collision.gameObject.CompareTag("Ground"))
            //{
            //    if (collision.gameObject.TryGetComponent(out Ground ground))
            //    {
            //        buffedSpeedRate = ground.speedRate;
            //    }
            //}

            int groundLayer = LayerMask.NameToLayer("Ground");
            if (collision.gameObject.layer == groundLayer)
            {
                if (collision.gameObject.TryGetComponent(out Ground ground))
                {
                    buffedSpeedRate = ground.speedRate;
                }
            }
        }
        private void OnCollisionStay(Collision collision) { }
        private void OnCollisionExit(Collision collision) { }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Gate"))
            {
                if (other.gameObject.TryGetComponent(out TriggerGate gate))
                {
                    if (currentGate != null && currentGate == gate)
                        return;

                    currentGate = gate;
                    currentHP -= gate.damage;

                    OnChangedHP?.Invoke(currentHP, maxHP);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("SpawnerField"))
            {
                if (other.gameObject.TryGetComponent(out SpawnerField spawnerField))
                {
                    spawnerField.Spawn();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Gate"))
            {
                if (other.gameObject.TryGetComponent(out TriggerGate gate))
                {
                    if (gate == currentGate)
                    {
                        currentGate = null;
                    }
                }
            }
        }
    }
}

