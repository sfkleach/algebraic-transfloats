namespace TransMaths;

public abstract class ITDouble
{
    public abstract ITDouble Add(ITDouble x);
    public abstract ITDouble Mul(ITDouble x);
    public abstract ITDouble Negate();
    public abstract ITDouble Recipocal();
    public abstract ITDouble AntiRecipocal();
    public abstract bool IsPositive();
    public abstract bool IsNegative();

    public ITDouble Sub(ITDouble x) => Add(x.Negate());

    public ITDouble Divide(ITDouble x) => Mul(x.Recipocal());

    public static readonly NonfiniteTDouble NULLITY = new(0);
    public static readonly NonfiniteTDouble PINF = new(1);
    public static readonly NonfiniteTDouble NINF = new(-1);
    public static readonly FiniteTDouble ZERO = new(0);

    /// <summary>
    /// Typically constructs a finite value but copes with overflow to +/- infinity.
    /// </summary>
    /// <param name="x">an arbitrary double</param>
    /// <returns>a transfloat</returns>
    public static ITDouble Make(double x) =>
        Double.IsNaN(x) ? NULLITY : new FiniteTDouble(x) : 
        Double.IsInfinity(x) ? new NonfiniteTDouble(Math.Sign(x)) :
        NULLITY;
}


public class NonfiniteTDouble : ITDouble
{
    /// <summary>
    /// The three non-finite values are modelled as "sign" arithmetic and their
    /// state is modelled as one of {-1, 0, 1}.
    /// </summary>
    public int Sign { get; }

    /// <summary>
    /// Constructs one of the three non-finite values, each of which corresponds
    /// to the sign information -1, 0 or 1. It is intended to be used with finite
    /// values, although +infinity and -infinity are accepted and NaN is not.
    /// </summary>
    /// <param name="sign">the finite double to be converted</param>
    public NonfiniteTDouble(double sign) : this(Math.Sign(sign)) { }

    /// <summary>
    /// Constructs one of the three non-finite values, each of which corresponds
    /// to the sign information -1, 0 or 1.
    /// </summary>
    /// <param name="sign">the integer to be converted</param>
    public NonfiniteTDouble(int sign) { Sign = Math.Sign(sign); }

    /// <summary>
    /// In sums involving non-finite values, a finite operand plays the role of zero.
    /// </summary>
    /// <param name="x">the transfloat operand</param>
    /// <returns>the sum</returns>
    public override ITDouble Add(ITDouble x) => 
        x is NonfiniteTDouble n ? new NonfiniteTDouble(Sign + n.Sign) : this;

    /// <summary>
    /// In products involving non-finite values, a finite operand plays the role of a sign.
    /// </summary>
    /// <param name="x">the transfloat operand</param>
    /// <returns>the product</returns>
    public override ITDouble Mul(ITDouble x) =>
        x is NonfiniteTDouble n ? new NonfiniteTDouble(Sign * n.Sign) : x.Mul(this);

    public override ITDouble Negate() => new NonfiniteTDouble(-Sign);

    public override ITDouble Recipocal() => Sign == 0 ? this : ZERO;

    public override ITDouble AntiRecipocal() => Recipocal();

    public override bool IsPositive() => Sign > 0;
    public override bool IsNegative() => Sign < 0;

    public override bool Equals(object? obj) => obj is NonfiniteTDouble x && x.Sign == Sign;

    public override int GetHashCode() => Sign.GetHashCode();
}


public class FiniteTDouble : ITDouble
{
    public double Value { get; }

    public FiniteTDouble(double value) { Value = value; }

    public override ITDouble Add(ITDouble x) => 
        x is FiniteTDouble n ? Make(Value + n.Value) : x;

    public override ITDouble Mul(ITDouble x) =>
        x is FiniteTDouble n ? Make(Value * n.Value) : new NonfiniteTDouble(Value).Mul(x);

    public override ITDouble Negate() => new FiniteTDouble(-Value);

    public override ITDouble Recipocal() =>
        Value == 0 ? PINF : Make(1/Value);

    public override ITDouble AntiRecipocal() =>
        Value == 0 ? NINF : Make(-1/Value);

    public override bool IsPositive() => Value > 0;
    public override bool IsNegative() => Value < 0;

    public override bool Equals(object? obj) => obj is FiniteTDouble x && x.Value == Value;

    public override int GetHashCode() => Value.GetHashCode();
}
