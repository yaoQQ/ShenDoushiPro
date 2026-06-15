public class IdCounter
{
    ushort counter = ushort.MinValue;

    public ushort GetNewId()
    {
        counter++;
        if (counter == ushort.MaxValue)
        {
            counter = ushort.MinValue;
        }
        return counter;
    }
}