using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandFactory
{
    [System.Serializable]
    public class CardProductType
    {
        [SerializeField]
        private GameObject _cardPrefab;

        public GameObject CardPrefab => _cardPrefab;

        [SerializeField]
        private int _amount;

        public int Amount => _amount;
    }
}


