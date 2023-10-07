using System;
using UnityEngine;

namespace Player
{
    
    
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;
        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
        {
             visualGameObject.SetActive(e.selectedCounter == clearCounter);
        }
    }
}