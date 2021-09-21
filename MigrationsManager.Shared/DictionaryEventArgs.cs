using MigrationsManager.Shared.Enumerations;

namespace MigrationsManager.Shared
{
    public class DictionaryEventArgs<TValue>
    {
        public EventType EventType { get; }

        public DictionaryEventArgs(EventType eventType, bool suceeded, TValue value)
        {
            EventType = eventType;
            Suceeded = suceeded;
            Value = value;
        }

        public bool Suceeded { get; }
        public TValue Value { get; }
    }
}
