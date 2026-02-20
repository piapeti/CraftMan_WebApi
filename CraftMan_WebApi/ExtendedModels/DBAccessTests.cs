using Xunit;
using Moq;
using System.Data;
using Microsoft.Data.SqlClient;
using CraftMan_WebApi.DataAccessLayer;
using CraftMan_WebApi.Models;
using Microsoft.Extensions.Configuration;
using System.Reflection;

public class DBAccessTests
{
    [Fact]
    public void InitializeConnection_OpensConnection()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>("FakeConnectionString");
        var dbAccess = new DBAccess();
        typeof(DBAccess)
            .GetField("strConnectionString", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, mockConnection.Object);

        mockConnection.Setup(c => c.Open());

        // Act
        dbAccess.InitializeConnection("FakeConnectionString");

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once);
    }

    [Fact]
    public void BeginTransaction_BeginsTransaction()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>("FakeConnectionString");
        var mockTransaction = new Mock<SqlTransaction>();
        mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);

        var dbAccess = new DBAccess();
        typeof(DBAccess)
            .GetField("strConnectionString", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, mockConnection.Object);

        // Act
        dbAccess.BeginTransaction();

        // Assert
        mockConnection.Verify(c => c.BeginTransaction(), Times.Once);
    }

    [Fact]
    public void ReadDB_OpensConnectionAndExecutesReader()
    {
        // Arrange
        var mockConnection = new Mock<SqlConnection>("FakeConnectionString");
        var mockCommand = new Mock<SqlCommand>();
        var mockReader = new Mock<SqlDataReader>();

        mockConnection.Setup(c => c.State).Returns(ConnectionState.Closed);
        mockConnection.Setup(c => c.Open());
        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);

        var dbAccess = new DBAccess();
        typeof(DBAccess)
            .GetField("strConnectionString", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, mockConnection.Object);

        // Act
        var result = dbAccess.ReadDB("SELECT 1");

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once);
        mockCommand.Verify(c => c.ExecuteReader(), Times.Once);
        Assert.Equal(mockReader.Object, result);
    }

    [Fact]
    public void Validate_ReturnsAlreadyExists_WhenRowsExist()
    {
        // Arrange
        var dbAccess = new DBAccess();
        var mockAdapter = new Mock<SqlDataAdapter>();
        var dt = new DataTable();
        dt.Rows.Add(dt.NewRow());

        // Use reflection to set strConnection
        typeof(DBAccess)
            .GetField("strConnection", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, "FakeConnectionString");

        // Act
        var result = dbAccess.validate("SELECT 1");

        // Assert
        Assert.Equal(1, result.StatusCode);
        Assert.Equal("Already exists...", result.StatusMessage);
    }

    [Fact]
    public void AddParameter_AddsParameterToCommand()
    {
        // Arrange
        var dbAccess = new DBAccess();
        var mockCommand = new Mock<SqlCommand>();
        mockCommand.SetupGet(c => c.Parameters).Returns(new SqlParameterCollectionStub());

        typeof(DBAccess)
            .GetField("sqlCmd", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, mockCommand.Object);

        // Act
        dbAccess.AddParameter("@param", "value");

        // Assert
        Assert.Single(((SqlParameterCollectionStub)mockCommand.Object.Parameters).Parameters);
    }

    [Fact]
    public void ExecuteScalar_ExecutesScalarAndReturnsResult()
    {
        // Arrange
        var dbAccess = new DBAccess();
        var mockConnection = new Mock<SqlConnection>("FakeConnectionString");
        typeof(DBAccess)
            .GetField("strConnectionString", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, mockConnection.Object);

        // Act & Assert
        // This method is hard to test without refactoring for dependency injection.
        // You may want to refactor DBAccess for better testability.
    }

    [Fact]
    public void ExecuteNonQuery_OpensConnectionAndExecutesNonQuery()
    {
        // Arrange
        var dbAccess = new DBAccess();
        var mockConnection = new Mock<SqlConnection>("FakeConnectionString");
        var mockCommand = new Mock<SqlCommand>();

        mockConnection.Setup(c => c.State).Returns(ConnectionState.Closed);
        mockConnection.Setup(c => c.Open());
        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
        mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

        typeof(DBAccess)
            .GetField("strConnectionString", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(dbAccess, mockConnection.Object);

        // Act
        var result = dbAccess.ExecuteNonQuery("UPDATE ...");

        // Assert
        mockConnection.Verify(c => c.Open(), Times.Once);
        mockCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
        Assert.Equal(1, result);
    }
}

// Helper stub for SqlParameterCollection
public class SqlParameterCollectionStub : SqlParameterCollection
{
    public List<SqlParameter> Parameters { get; } = new List<SqlParameter>();
    public override int Add(object value)
    {
        Parameters.Add((SqlParameter)value);
        return Parameters.Count - 1;
    }
    // Implement other abstract members as needed for your tests
}
