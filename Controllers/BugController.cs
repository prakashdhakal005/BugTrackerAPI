using BugTrackerAPI.Application.Dtos;
using BugTrackerAPI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BugTrackerAPI.Domain.Application;

namespace BugTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/bugs")]
    [Authorize]
    public class BugController : ControllerBase
    {
        private readonly IBugService _bugService;

        public BugController(IBugService bugService)
        {
            _bugService = bugService;
        }

        [HttpPost("create")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Create(BugCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = "0e00d274-7885-47f7-bfbf-dca777e65443";
            var bugId = await _bugService.CreateBugAsync(dto, userId);
            return Ok(new { bugId });
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = AppRoles.Developer)]
        public async Task<IActionResult> UpdateStatus(int id, BugUpdateStatusDto dto)
        {
            var devId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _bugService.UpdateBugStatusAsync(id, dto.Status, devId);
            return Ok();
        }

        [HttpGet("search")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Search([FromQuery] BugSearchDto dto)
        {
            var bugs = await _bugService.SearchUnassignedBugsAsync(dto);
            return Ok(bugs);
        }

        [HttpGet("getAll")]
        [Authorize(Roles = $"{AppRoles.User},{AppRoles.Developer}")]
        public async Task<IActionResult> GetAll()
        {
            var bugs = await _bugService.GetAllBugsAsync();
            return Ok(bugs);
        }

        [HttpGet("my")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> GetMyBugs()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bugs = await _bugService.GetBugsByUserIdAsync(userId);
            return Ok(bugs);
        }

        [HttpPut("{id}/assign")]
        [Authorize(Roles = AppRoles.Developer)]
        public async Task<IActionResult> AssignToMe(int id)
        {
            var developerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _bugService.AssignBugToDeveloperAsync(id, developerId);

            return Ok("Bug assigned successfully");
        }

        [HttpGet("assigned")]
        [Authorize(Roles = AppRoles.Developer)]
        public async Task<IActionResult> GetAssignedToMe()
        {
            var developerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bugs = await _bugService
                .GetAssignedBugsForDeveloperAsync(developerId);

            return Ok(bugs);
        }

    }

}
