using System.Text;
using OM.Main;

namespace OM.Test;

[TestClass]
public class IntegrationTests
{
    [TestMethod]
    public async Task TestMethod1()
    {
        // Arrange

        // Act
        await Program.Main(args: [ "-f" ]);

        // Assert
    }

    [TestMethod]
    public async Task Main_Should_Display_Error_Message_When_There_Is_No_Argument()
    {
        // Arrange
        var sb = new StringBuilder();
        using var sw = new StringWriter(sb);
        Console.SetOut(newOut: sw);

        // Act
        await Program.Main(args: []);

        // Assert
        Assert.IsTrue(sb.ToString().Contains("Missing arguments."));
    }

    [DataTestMethod]
    [DataRow("-t", null)]
    [DataRow("-s", null)]
    [DataRow("-s", "2024-11-31")]
    public async Task Main_Should_Display_Error_Message_When_Arguments_Are_Invalid(string command, string? date)
    {
        // Arrange
        var sb = new StringBuilder();
        using var sw = new StringWriter(sb);
        Console.SetOut(newOut: sw);

        List<string> args = [ command ];
        if (date != null)
        {
            args.Add(date);
        }

        // Act
        await Program.Main(args: [.. args]);

        // Assert
        Assert.IsTrue(sb.ToString().Contains("Invalid arguments."));
    }
}