using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ObservableCollection<Item> items = new ObservableCollection<Item>();
    public static Inventory Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item) {
        items.Add(item);
    }
}
