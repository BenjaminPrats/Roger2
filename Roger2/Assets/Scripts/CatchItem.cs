using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CatchItem : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Refill,
        Booster,
        Coin,
        Star
    };

    public float _reloadAmount = 0.5f;

    public ItemType itemType = ItemType.None;

    public AudioSource _sound;

    //private void Start()
    //{
    //    _sound = GetComponent<AudioSource>();
    //}

    private void OnTriggerEnter(Collider other)
    {
        CarController carController = other.gameObject.transform.parent.gameObject.GetComponent<CarController>();
        if (carController)
        {
            _sound.transform.parent = null;
            _sound.Play();
            Destroy(_sound.gameObject, _sound.clip.length);

            carController._scoreManager.AddItem(itemType);

            Debug.Log("Catch: " + itemType);

            carController.ReloadBoost(_reloadAmount);
            gameObject.SetActive(false);
        }
    }




}
