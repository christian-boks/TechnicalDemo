namespace BackendApi.Utils;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException() : base() { }
}

public class UnknownErrorException : Exception
{
    public UnknownErrorException() : base() { }
}

public class NotFoundException : Exception
{
    public NotFoundException() : base() { }
}
