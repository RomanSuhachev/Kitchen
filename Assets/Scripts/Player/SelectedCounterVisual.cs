using System;
using UnityEngine;

namespace Player
{
    
    
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter baseCounter;
        [SerializeField] private GameObject[] visualGameObjectArray;
        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
        {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(e.selectedCounter == baseCounter);
            }
             
        }
    }
}
