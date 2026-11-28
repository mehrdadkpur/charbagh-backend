using Charbagh.Api.Authorization;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BannersController : ControllerBase
{
    private readonly CharbaghDbContext _context;

    public BannersController(CharbaghDbContext context)
    {
        _context = context;
    }

    [HttpGet("active")]
    [AllowAnonymous]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var banners = await _context.AnnouncementBanners
            .AsNoTracking()
            .Where(x => x.Status == BannerStatus.Published
                && (x.StartsAt == null || x.StartsAt <= now)
                && (x.EndsAt == null || x.EndsAt > now))
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return Ok(banners.Select(ToDto));
    }

    [HttpGet]
    [HasPermission("Banners", PermissionAction.View)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var banners = await _context.AnnouncementBanners
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return Ok(banners.Select(ToDto));
    }

    [HttpGet("{id:guid}")]
    [HasPermission("Banners", PermissionAction.View)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var banner = await _context.AnnouncementBanners
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return banner is null ? NotFound() : Ok(ToDto(banner));
    }

    [HttpPost]
    [HasPermission("Banners", PermissionAction.Create)]
    public async Task<IActionResult> Create(
        SaveAnnouncementBannerRequest request,
        CancellationToken cancellationToken)
    {
        var banner = AnnouncementBanner.Create(
            request.Title,
            request.Description,
            request.ImageUrl,
            request.LinkUrl,
            request.ButtonText,
            request.StartsAt,
            request.EndsAt,
            request.SortOrder);

        await _context.AnnouncementBanners.AddAsync(banner, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = banner.Id }, ToDto(banner));
    }

    [HttpPut("{id:guid}")]
    [HasPermission("Banners", PermissionAction.Update)]
    public async Task<IActionResult> Update(
        Guid id,
        SaveAnnouncementBannerRequest request,
        CancellationToken cancellationToken)
    {
        var banner = await _context.AnnouncementBanners
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (banner is null) return NotFound();

        banner.Update(
            request.Title,
            request.Description,
            request.ImageUrl,
            request.LinkUrl,
            request.ButtonText,
            request.StartsAt,
            request.EndsAt,
            request.SortOrder);

        await _context.SaveChangesAsync(cancellationToken);
        return Ok(ToDto(banner));
    }

    [HttpPatch("{id:guid}/publish")]
    [HasPermission("Banners", PermissionAction.Approve)]
    public Task<IActionResult> Publish(Guid id, CancellationToken cancellationToken) =>
        ChangeStatus(id, x => x.Publish(), cancellationToken);

    [HttpPatch("{id:guid}/unpublish")]
    [HasPermission("Banners", PermissionAction.Approve)]
    public Task<IActionResult> Unpublish(Guid id, CancellationToken cancellationToken) =>
        ChangeStatus(id, x => x.Unpublish(), cancellationToken);

    [HttpPatch("{id:guid}/archive")]
    [HasPermission("Banners", PermissionAction.Delete)]
    public Task<IActionResult> Archive(Guid id, CancellationToken cancellationToken) =>
        ChangeStatus(id, x => x.Archive(), cancellationToken);

    [HttpPatch("{id:guid}/restore")]
    [HasPermission("Banners", PermissionAction.Update)]
    public Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken) =>
        ChangeStatus(id, x => x.Restore(), cancellationToken);

    private async Task<IActionResult> ChangeStatus(
        Guid id,
        Action<AnnouncementBanner> change,
        CancellationToken cancellationToken)
    {
        var banner = await _context.AnnouncementBanners
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (banner is null) return NotFound();

        change(banner);
        await _context.SaveChangesAsync(cancellationToken);
        return Ok(ToDto(banner));
    }

    private static AnnouncementBannerDto ToDto(AnnouncementBanner banner) =>
        new(
            banner.Id,
            banner.Title,
            banner.Description,
            banner.ImageUrl,
            banner.LinkUrl,
            banner.ButtonText,
            banner.StartsAt,
            banner.EndsAt,
            banner.SortOrder,
            banner.Status,
            banner.CreatedAt,
            banner.UpdatedAt);
}

public sealed record SaveAnnouncementBannerRequest(
    string Title,
    string? Description,
    string ImageUrl,
    string? LinkUrl,
    string? ButtonText,
    DateTime? StartsAt,
    DateTime? EndsAt,
    int SortOrder);

public sealed record AnnouncementBannerDto(
    Guid Id,
    string Title,
    string? Description,
    string ImageUrl,
    string? LinkUrl,
    string? ButtonText,
    DateTime? StartsAt,
    DateTime? EndsAt,
    int SortOrder,
    BannerStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
