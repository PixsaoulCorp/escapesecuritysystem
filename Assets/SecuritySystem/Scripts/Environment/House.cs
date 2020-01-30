using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixsaoul.Security
{
    public class House : MonoBehaviour
    {
        public List<Room> _rooms;

        public Color _unknownColor;
        public Color _lockedColor;
        public Color _unlockedColor;
        public Color _availableColor;


        public void Initialize()
        {
            foreach(Room r in _rooms)
            {
                r.Initialize(this);
            }
        }
    }
}
