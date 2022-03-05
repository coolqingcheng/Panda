namespace Panda.Tools;

public class TestPanda
{
    public static void SayEnv()
    {
#if DEBUG
        Console.WriteLine("debug");
#endif
#if RELEASE
        Console.WriteLine("RELEASE");
#endif
    }
}