using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {
        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [HttpPost]
        public IActionResult Calculate(CommissionCalculationRequest calculationRequest)
        {
            if (calculationRequest == null)
            {
                return BadRequest(new { error = "Request body cannot be null." });
            }

            if (calculationRequest.LocalSalesCount < 0 ||
                calculationRequest.ForeignSalesCount < 0 ||
                calculationRequest.AverageSaleAmount < 0)
            {
                return BadRequest(new { error = "All input values must be zero or greater." });
            }

            if (calculationRequest.LocalSalesCount > 100000 || calculationRequest.ForeignSalesCount > 100000)
            {
                return BadRequest(new { error = "Sales counts are unrealistically high." });
            }

            if (calculationRequest.AverageSaleAmount > 1000000)
            {
                return BadRequest(new { error = "Average sale amount exceeds reasonable limit." });
            }

            const decimal avalphaLocalRate = 0.20m;
            const decimal avalphaForeignRate = 0.35m;

            const decimal competitorLocalRate = 0.02m;
            const decimal competitorForeignRate = 0.0755m;

            var avalphaLocalCommission = calculationRequest.LocalSalesCount * calculationRequest.AverageSaleAmount * avalphaLocalRate;
            var avalphaForeignCommission = calculationRequest.ForeignSalesCount * calculationRequest.AverageSaleAmount * avalphaForeignRate;
            var avalphaTotal = avalphaLocalCommission + avalphaForeignCommission;

            var competitorLocalCommission = calculationRequest.LocalSalesCount * calculationRequest.AverageSaleAmount * competitorLocalRate;
            var competitorForeignCommission = calculationRequest.ForeignSalesCount * calculationRequest.AverageSaleAmount * competitorForeignRate;
            var competitorTotal = competitorLocalCommission + competitorForeignCommission;

            return Ok(new CommissionCalculationResponse
            {
                AvalphaTechnologiesCommissionAmount = Math.Round(avalphaTotal, 2),
                CompetitorCommissionAmount = Math.Round(competitorTotal, 2)
            });
        }
    }

    public class CommissionCalculationRequest
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }

    public class CommissionCalculationResponse
    {
        public decimal AvalphaTechnologiesCommissionAmount { get; set; }

        public decimal CompetitorCommissionAmount { get; set; }
    }
}
