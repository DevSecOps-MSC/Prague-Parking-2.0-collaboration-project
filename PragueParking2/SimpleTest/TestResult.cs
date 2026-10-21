namespace PragueParking2.SimpleTest;

public class TestResult(string description, bool result)
{
    public string Description { get; private set; } = description;

    public bool Result { get; private set; } = result;

    public bool IsPositive { get => Result == true; }

    public bool IsNegative { get => Result == false; }
}
