using GameSystem.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandFactory
{
    public class CardEventArgs : EventArgs
    {

        public CardView CardView { get; }

        public CardEventArgs(CardView cardView)
        {
            CardView = cardView;
        }

    }

    public class HandView : MonoBehaviour
    {
        public event EventHandler<CardEventArgs> CardStateSwitched;

        [SerializeField]
        private CardProductType[] _productTypes;

        private List<GameObject> _hand;

        public void ChildStateSwitched(CardView cardView)
        {
            OnStateSwitched(new CardEventArgs(cardView));
        }

        public void RemoveCard(CardView cardView)
        {
            Destroy(cardView.gameObject);
        }

        public void OnStateSwitched(CardEventArgs eventArgs)
        {
            EventHandler<CardEventArgs> handler = CardStateSwitched;
            handler?.Invoke(this, eventArgs);
        }

    }
}


