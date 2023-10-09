using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

   private const string OPEN_CLOSE = "OpenClose";
   [SerializeField] private ContainerCounter containerCounter;
   private Animator _animator;
   private static readonly int OpenClose = Animator.StringToHash(OPEN_CLOSE);

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Start()
   {
      containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
   }

   private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
   {
      _animator.SetTrigger(OpenClose);
   }
}
