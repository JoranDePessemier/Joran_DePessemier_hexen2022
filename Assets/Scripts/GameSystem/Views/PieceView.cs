using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour
    {
        [SerializeField]
        private PieceType _type;

        public PieceType Type => _type;

        public Vector3 WorldPosition => transform.position;

        //when a piece is first spawned on the board
        internal void Placed(Vector3 worldPosition)
        {
            gameObject.SetActive(true);
            transform.position = worldPosition;
        }

        //when the board class moves a piece to a new position
        internal void MoveTo(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        //when the piece needs to get removed
        internal void Taken()
        {
            gameObject.SetActive(false);
        }
    }

}
