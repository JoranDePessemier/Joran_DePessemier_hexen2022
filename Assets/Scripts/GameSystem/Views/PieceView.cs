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

        internal void Placed(Vector3 worldPosition)
        {
            gameObject.SetActive(true);
            transform.position = worldPosition;
        }

        internal void MoveTo(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        internal void Taken()
        {
            gameObject.SetActive(false);
        }
    }

}
