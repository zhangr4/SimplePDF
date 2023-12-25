namespace SimplePDF.Core;

public interface IGuardClause { }

public class Guard : IGuardClause
{
    private Guard() { }

    public static IGuardClause Against { get; } = new Guard();
}

public static class GuardClauseExtensions
{
    public static T Null<T>(this IGuardClause guardClause, T input, string parameterName)
    {
        if (input is null)
            throw new ArgumentNullException(parameterName);

        return input;
    }

    public static void FileNotExist(this IGuardClause guardClause, string filePath, string parameterName)
    {
        if(!File.Exists(filePath)) throw new FileNotFoundException(parameterName);
    }
}