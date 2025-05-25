using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Rigidbody _bulletRb;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _rotration;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        GameObject bulletInstance = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);

        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = _bulletPoint.forward * _bulletSpeed;
        }
        else
        {
            Debug.LogError("ѕуле нет Rigidbody");
            Destroy(bulletInstance);
        }
        }
    }

}
