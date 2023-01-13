using GameSystem.Views;
using System;
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

        [SerializeField]
        private int _handSize;

        [SerializeField]
        private Canvas _dragCanvas;

        private List<GameObject> _pile = new List<GameObject>();

        private void Start()
        {
            //for the producttypes given in engine, put the right cards in the pile
            foreach(CardProductType type in _productTypes)
            {
                for (int i = 0; i < type.Amount; i++)
                {
                    GameObject card = GameObject.Instantiate(type.CardPrefab);
                    card.name = $"{type.CardPrefab.name}:{i}";
                    card.GetComponent<CardView>().Canvas = _dragCanvas;
                    card.transform.SetParent(this.transform);
                    card.SetActive(false);
                    

                    _pile.Add(card);
                }
            }

            //place the cards in the hands from the pile according to how big the hand should be (set up in engine)
            for (int i = 0; i < _handSize; i++)
            {
                PutCardInHand();
            }
        }

        //place a card from the pile in the hand => only works if the pile is not empty
        private void PutCardInHand()
        {
            if(_pile.Count > 0)
            {
                GameObject card = _pile[UnityEngine.Random.Range(0, _pile.Count)];
                card.SetActive(true);
                _pile.Remove(card);
            }
        }

        //if a card is being dragged or dropped
        public void ChildStateSwitched(CardView cardView)
        {
            OnStateSwitched(new CardEventArgs(cardView));
        }

        //remove a card entirely from the hand and pile => automatically places a new card in the hand
        public void RemoveCard(CardView cardView)
        {
            Destroy(cardView.gameObject);
            PutCardInHand();
        }

        public void OnStateSwitched(CardEventArgs eventArgs)
        {
            EventHandler<CardEventArgs> handler = CardStateSwitched;
            handler?.Invoke(this, eventArgs);
        }

    }
}


