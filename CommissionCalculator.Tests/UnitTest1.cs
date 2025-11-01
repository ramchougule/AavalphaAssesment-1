using AvalphaTechnologies.CommissionCalculator.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CommissionCalculator.Tests;

public class UnitTest1
{
    [Fact]
    public void Calculate_ShouldReturnCorrectOutput_ForValidInput()
    {
        //Arrange
        var controller = new CommisionController();
        var request = new CommissionCalculationRequest
        {
            LocalSalesCount = 10,
            ForeignSalesCount = 10,
            AverageSaleAmount = 100
        };

        //Act
        var result = controller.Calculate(request);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = okResult.Value as CommissionCalculationResponse;

        Assert.NotNull(response);
        Assert.Equal(550m, response.AvalphaTechnologiesCommissionAmount);
        Assert.Equal(95.5m, response.CompetitorCommissionAmount);
    }

    [Theory]
    [InlineData(-1, 10, 100)]
    [InlineData(10, -5, 100)]
    [InlineData(10, 10, -100)]
    public void Calculate_ShouldReturnBadRequest_ForInValidInput(int localSalesCount, int foreignSalesCount, decimal averageSaleCount)
    {
        // Arrange
        var controller = new CommisionController();
        var request = new CommissionCalculationRequest
        {
            LocalSalesCount = localSalesCount,
            ForeignSalesCount = foreignSalesCount,
            AverageSaleAmount = averageSaleCount
        };

        // Act
        var result = controller.Calculate(request) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }
}
