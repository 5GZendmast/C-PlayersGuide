using System;
using System.Linq;

int itemNumber = 1;

//-------------- Create a Backpack ----------------------------------------

Pack Backpack = new Pack();

//-------------- Run Item selection loop ---------------------------------

while (true)
{
    int choice;
    InventoryItem addItem;


    Console.WriteLine("-------- Backpack Propperties--------");
    Console.WriteLine($"Max. items: {Backpack.itemAmount}");
    Console.WriteLine($"Max. weight: {Backpack.maxWeight}");
    Console.WriteLine($"Max. volume: {Backpack.maxVolume}");
    Console.WriteLine("-------------------------------------");
    Console.WriteLine($"Current item selection: {itemNumber}");
    Console.WriteLine($"Current weight: {Backpack.WeightArray.Sum()}");
    Console.WriteLine($"Current volume: {Backpack.VolumeArray.Sum()}");
    Console.WriteLine("-------------------------------------");
    Console.WriteLine("Choose an Item:");
    Console.WriteLine("1.   Arrow");
    Console.WriteLine("2.   Bow");
    Console.WriteLine("3.   Rope");
    Console.WriteLine("4.   Food");
    Console.WriteLine("5.   Water");
    Console.WriteLine("6.   Sword");
    Console.WriteLine("-------------------------------------");

    Console.ForegroundColor = ConsoleColor.Red;
    choice = Convert.ToInt32(Console.ReadLine());
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Clear();

    addItem = choice switch
    {
        1 => new Arrow(),
        2 => new Bow(),
        3 => new Rope(),
        4 => new Food(),
        5 => new Water(),
        6 => new Sword()
    };

    if (Backpack.Add(addItem, itemNumber) == false)
    {
        break;
    }
    else
    {
        itemNumber++;
    }
}

Console.WriteLine($"You can't do that");
Console.WriteLine("Press any key to view your backpack");
Console.WriteLine("-------------------------------------");
Console.ReadKey(true);
Console.WriteLine($"Items in backpack: {Backpack.ItemArray.Length}");
Console.WriteLine($"Current weight: {Backpack.WeightArray.Sum()}");
Console.WriteLine($"Current volume: {Backpack.VolumeArray.Sum()}");

//--------------- Backpack Class ---------------------------------------

public class Pack
{
    public int itemAmount { get; protected set; }
    public float maxWeight { get; protected set; }
    public float maxVolume { get; protected set; }
    public int arrayLenght { get; set; } = 0;
    public InventoryItem[] ItemArray {get; set;}
    public float[] WeightArray { get; set; }
    public float[] VolumeArray { get; set; }


    public Pack()
    {
        Console.WriteLine("What is the max. item amount in the backpack?");
        itemAmount = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        ItemArray = new InventoryItem[itemAmount];
        WeightArray = new float[itemAmount];
        VolumeArray = new float[itemAmount];

        Console.WriteLine("What is the max. weight of the backpack?");
        maxWeight = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        Console.Write("What is the max. volume of the backpack?");
        maxVolume = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

    }

    public bool Add(InventoryItem addItem, int selection)
    {
        ItemArray[selection - 1] = addItem;
        WeightArray[selection - 1] = addItem.Weight;
        VolumeArray[selection - 1] = addItem.Volume;

        if (selection >= itemAmount)
        {
            return false;
        }

        if (WeightArray.Sum() > maxWeight)
        {
            return false;
        }

        if(VolumeArray.Sum() > maxVolume)
        {
            return false;
        }

        return true;

    }
}

//--------------- Base InventoryItem Class ----------------------------

public class InventoryItem
{
    public float Weight { get; }
    public float Volume { get; }

    public InventoryItem(float weight, float volume)
    {
        Weight = weight;
        Volume = volume;
    }
}

//--------------- Derived Item Classes -------------------------------

public class Arrow : InventoryItem
{
    public Arrow() : base(0.1f, 0.05f) {}
}

public class Bow : InventoryItem
{
    public Bow() : base(1, 4) { }
}

public class Rope : InventoryItem
{
    public Rope() : base(1, 1.5f) { }
}

public class Food : InventoryItem
{
    public Food() : base(1, 0.5f) { }
}

public class Water : InventoryItem
{
    public Water() : base(2, 3) { }
}

public class Sword : InventoryItem
{
    public Sword() : base(5, 3) { }
}

//---------------------------------------------------------------------

