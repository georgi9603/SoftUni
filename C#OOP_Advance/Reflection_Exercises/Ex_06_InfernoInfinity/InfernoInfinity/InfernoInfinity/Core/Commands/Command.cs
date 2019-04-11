
public abstract class Command : IExecutable
{
    private string[] data;

    protected Command(string[] data)
    {
        this.data = data;
    }

    protected string[] Data => data;
    public abstract void Execute();
}