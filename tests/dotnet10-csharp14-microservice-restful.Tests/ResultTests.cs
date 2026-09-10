using dotnet10.csharp14.microservice.restful.Application.Common;
using Xunit;

namespace dotnet10.csharp14.microservice.restful.Tests;

public class ResultTests
{
    [Fact]
    public void Success_CreaResultadoConValor()
    {
        var res = Result<int>.Success(42);

        Assert.True(res.IsSuccess);
        Assert.Equal(42, res.Value);
        Assert.Null(res.Error);
    }

    [Fact]
    public void Failure_CreaResultadoConError()
    {
        var res = Result<string>.Failure("oops");

        Assert.False(res.IsSuccess);
        Assert.Null(res.Value);
        Assert.Equal("oops", res.Error);
    }
}
