namespace Rhyous.Collections.Tests
{
    public class EntityWithCaseDifferentProps
    {
        public int SomeId { get; set; }
        public int someId { get; set; }
        public long someid { get; set; }
    }
}