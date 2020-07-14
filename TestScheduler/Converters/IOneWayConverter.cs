namespace TestScheduler.Converters
{
    public interface IOneWayConverter<in TIn, out TOut>
    {
        TOut Convert(TIn input);
    }
}