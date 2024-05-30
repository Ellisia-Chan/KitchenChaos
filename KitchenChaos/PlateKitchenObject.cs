using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOlist;


    private void Awake() {
        kitchenObjectSOlist = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            //Not a valid ingredient
            return false;
        }
        if (kitchenObjectSOlist.Contains(kitchenObjectSO)) {
            // Already has this type
            return false;
        } else {
            kitchenObjectSOlist.Add(kitchenObjectSO);
            return true;
        }
    }
}
