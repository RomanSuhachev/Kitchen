using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress

{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()

    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:

                    fryTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);


                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitechenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = this.state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = this.state
                        });
                    }

                    break;
                case State.Burned:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                    break;
            }
        }
    }

    public override void Interact(Player.Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasReceipeWithInput(player.GetKitchenObject().GetKitechenObjectSO()))
                {
                    //Player carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitechenObjectSO());

                    state = State.Frying;
                    fryTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
                else
                {
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //Player is holding a Plate
                        if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitechenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                            state = State.Idle;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = this.state
                            });

                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = fryTimer / fryingRecipeSO.fryingTimerMax
                            });
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(player);

                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = this.state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
            }
        }
    }

    private bool HasReceipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSo in fryingRecipeSOArray)
        {
            if (fryingRecipeSo.input == inputKitchenObjectSO)
            {
                return fryingRecipeSo;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSo in burningRecipeSOArray)
        {
            if (burningRecipeSo.input == inputKitchenObjectSO)
            {
                return burningRecipeSo;
            }
        }

        return null;
    }
}