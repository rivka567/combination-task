using Combination.BLL.Interfaces;
using Combination.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class CombinationController : ControllerBase
{
    private readonly ICombinationBL _combinationBL;
    private readonly CombinationService _combinationService;
    private readonly ILogger<CombinationController> _logger;

    public CombinationController(
        ICombinationBL combinationBL,
        CombinationService combinationService,
        ILogger<CombinationController> logger)
    {
        _combinationBL = combinationBL;
        _combinationService = combinationService;
        _logger = logger;
    }

    [HttpGet("Start")]
    public IActionResult Start([FromQuery] int n)
    {

        if (n < 1 || n > 20)
        {
            var message = "Value of n must be between 1 and 20";
            _logger.LogWarning(message);
            return BadRequest(message);
        }

        var totalCombinations = _combinationBL.CalcTotalCombinations(n);
        _logger.LogInformation("Total combinations calculated: {totalCombinations}", totalCombinations);

        return Ok(totalCombinations);
    }

    [HttpGet("GetNext")]
    public IActionResult GetNext([FromQuery] int index)
    {
        _logger.LogInformation("GetNext method called with parameter index: {index}", index);

        var total = _combinationService.GetTotalCombinations();
        if (!total.HasValue)
        {
            var message = "TotalCombinations equals null";
            _logger.LogError(message);
            return BadRequest(message);
        }

        var n = _combinationService.GetN();
        if (!n.HasValue)
        {
            var message = "N equals null";
            _logger.LogError(message);
            return BadRequest(message);
        }

        var combination = _combinationBL.GetNextCombination(index, n.Value, total.Value);
        _logger.LogInformation("Combination generated for index {index}: {combination}", index, combination);

        return Ok(combination);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        _logger.LogInformation("GetAll method called with pageIndex: {pageIndex}, pageSize: {pageSize}", pageIndex, pageSize);

        var total = _combinationService.GetTotalCombinations();
        if (!total.HasValue)
        {
            var message = "TotalCombinations equals null";
            _logger.LogError(message);
            return BadRequest(message);
        }

        var n = _combinationService.GetN();
        if (!n.HasValue)
        {
            var message = "N equals null";
            _logger.LogError(message);
            return BadRequest(message);
        }

        var combinations = _combinationBL.GetCombinationsByPage(pageIndex, pageSize, n.Value, total.Value);
        _logger.LogInformation("Combinations generated for pageIndex {pageIndex}, pageSize {pageSize}", pageIndex, pageSize);

        return Ok(combinations);
    }
}
