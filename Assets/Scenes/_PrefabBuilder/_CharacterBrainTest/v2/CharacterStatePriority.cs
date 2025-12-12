// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.



namespace CharacterNPC.v2
{
    public enum CharacterStatePriority
    {
        // Enums are ints starting at 0 by default.
        // This means you can compare them with numerical operators like < and >.
        None, // None = 0,
        Low,// Could specify "Low = 1," if we want to be explicit or change the order.
        MediumLow,// MediumLow = 2,
        Medium,// Medium = 3,
        MediumHigh,// MediumHigh = 4,
        High,// High = 5,
        Critical// Critical = 6
    }
}
