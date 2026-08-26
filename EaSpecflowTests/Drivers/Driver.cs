namespace EaSpecflowTests.Drivers;

public class Calculator
{
    private readonly List<int> _values = new();

    public void Enter(int number) => _values.Add(number);

    public int Add() => _values.Sum();
}
