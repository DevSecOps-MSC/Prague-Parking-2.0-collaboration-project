namespace SimpleTestLibrary;

public class Test(string description, Func<bool> testFunc)
{
    public string Description { get; } = description;

    public Func<bool> TestFunc { get; } = testFunc;

    public bool Perform()
    {
        return TestFunc();
    }
}
