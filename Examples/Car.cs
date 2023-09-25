namespace SqlExtensionsTester.Examples
{
    public class Car
    {
        public int Size { get; set; }
        public string Color { get; set; }
        public string Placas { get; set; }

        public Person Dueno { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}
