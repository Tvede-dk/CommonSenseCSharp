using System;

#region Ints

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public class IntRangeAttribute : Attribute{
    private readonly int _minValue;
    private readonly int _maxValue;

    public IntRangeAttribute(int minValue, int maxValue) // url is a positional parameter
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}

[AttributeUsage(AttributeTargets.All)]
public class PositiveIntRangeAttribute : IntRangeAttribute{
    public PositiveIntRangeAttribute() : base(0, int.MaxValue){
    }
}

#endregion

#region Long

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public class LongRangeAttribute : Attribute{
    private readonly long _minValue;
    private readonly long _maxValue;

    public LongRangeAttribute(long minValue, long maxValue){
        _minValue = minValue;
        _maxValue = maxValue;
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public class PositiveLongRangeAttribute : LongRangeAttribute{
    public PositiveLongRangeAttribute() : base(0, long.MaxValue){
    }
}

#endregion


#region Doubles

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public class DoubleRangeAttribute : Attribute{
    private readonly double _minValue;
    private readonly double _maxValue;

    public DoubleRangeAttribute(double minValue, double maxValue) // url is a positional parameter
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter |
                AttributeTargets.ReturnValue)]
public class PositiveDoubleRangeAttribute : DoubleRangeAttribute{
    public PositiveDoubleRangeAttribute() : base(0, double.MaxValue){
    }
}

#endregion
