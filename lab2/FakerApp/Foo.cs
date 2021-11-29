namespace FakerApp
{
    public class Foo
    {
        public string Data { get; private set; }
        public int Number { get; private set; }
        public string Info { get; set; }
        public int Id { get; set; }

        public Bar B { get; set; }

        public int Field = 2;
    }
}