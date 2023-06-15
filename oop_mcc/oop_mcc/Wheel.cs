public class Wheel : Vehicle
{
    public int wheel;

    public Wheel(int wheel, string name, string type, string color) : base(name, type, color)
    {
        //parent class
        this.name = name;
        this.type = type;
        this.color = color;

        //Child Class
        this.wheel = wheel;
    }

    public void Ban()
    {
        base.Spesification();
        Console.WriteLine("wheel : " + wheel);
    }
}