using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public class RangeAttribute : Attribute
{
    private readonly int _minValue;
    private readonly int _maxValue;

    public RangeAttribute(int minValue, int maxValue) // url is a positional parameter
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}

[AttributeUsage(AttributeTargets.All)]
public class PositiveIntAttribute : RangeAttribute
{
    public PositiveIntAttribute() : base(0, int.MaxValue)
    {
    }
}
