using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandFactory
{
    //class that is only used for the handview class. This is an easy way to spawn multiple of the same card in the hand. This needs to be configured in the engine

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


