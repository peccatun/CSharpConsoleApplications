public sealed class Box
{
    public Box(int number, int content)
    {
        Number = number;
        Content = content;
    }

    public int Number { get; set; }

    public int Content { get; set; }

    public bool IsOpened { get; set; }

    public override string ToString()
    {
        return $"{Number} -> {Content}";
    }
}