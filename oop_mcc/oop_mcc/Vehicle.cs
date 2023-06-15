public class Vehicle
{
    public string name;
    public string type;
    public string color;

    public Vehicle(string name, string type, string color)
    {
        this.name = name;
        this.type = type;
        this.color = color;
    }

    public void Spesification()
    {
        Console.WriteLine(" ");
        Console.WriteLine("Name : " + name);
        Console.WriteLine("Type : " + type);
        Console.WriteLine("Color : " + color);
    }

}