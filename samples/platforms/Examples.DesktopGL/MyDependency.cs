namespace Examples.DesktopGL;

public interface IMyDependency
{
    public int MyProperty { get; }
}

public class MyDependency : IMyDependency
{
    public int MyProperty => 42;
}