using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NetCore_Web_App.Data;
using ASP.NetCore_Web_App.Models;

namespace ASP.NetCore_Web_App.Pages.Contacts;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public List<Contact> Contacts { get; set; } = new();

    [BindProperty]
    public Contact ContactInput { get; set; } = new();

    // Statistics
    public int TotalContactsCount { get; set; }
    public int PersonalContactsCount { get; set; }
    public int WorkContactsCount { get; set; }
    public int OtherContactsCount { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }

    [TempData]
    public string? StatusToast { get; set; }

    [TempData]
    public string? StatusToastType { get; set; } // "success", "danger", "warning"

    public async Task OnGetAsync()
    {
        var userName = GetUserIdOrName();

        // Base query for user's contacts
        var query = _context.Contacts.Where(c => c.CreatedBy == userName);

        // Fetch overall stats for this user
        var allUserContacts = await query.ToListAsync();
        TotalContactsCount = allUserContacts.Count;
        PersonalContactsCount = allUserContacts.Count(c => c.Category == "Personal");
        WorkContactsCount = allUserContacts.Count(c => c.Category == "Work");
        OtherContactsCount = allUserContacts.Count(c => c.Category == "Other");

        // Apply search filter if specified
        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            var search = SearchQuery.Trim().ToLower();
            query = query.Where(c => 
                c.FirstName.ToLower().Contains(search) || 
                c.LastName.ToLower().Contains(search) || 
                c.Email.ToLower().Contains(search) || 
                c.Phone.Contains(search) ||
                (c.Company != null && c.Company.ToLower().Contains(search))
            );
        }

        // Apply category filter if specified
        if (!string.IsNullOrWhiteSpace(SelectedCategory) && SelectedCategory != "All")
        {
            query = query.Where(c => c.Category == SelectedCategory);
        }

        Contacts = await query.OrderBy(c => c.FirstName).ThenBy(c => c.LastName).ToListAsync();
    }

    public async Task<IActionResult> OnPostAddContactAsync()
    {
        var userName = GetUserIdOrName();

        if (string.IsNullOrEmpty(userName))
        {
            StatusToast = "Failed to add contact. Session expired or user not authenticated.";
            StatusToastType = "danger";
            return RedirectToPage(new { SearchQuery, SelectedCategory });
        }

        ContactInput.CreatedBy = userName;
        ContactInput.CreatedAt = DateTime.UtcNow;

        if (!ModelState.IsValid)
        {
            StatusToast = "Failed to add contact. Please check your inputs.";
            StatusToastType = "danger";
            return RedirectToPage(new { SearchQuery, SelectedCategory });
        }

        try
        {
            _context.Contacts.Add(ContactInput);
            await _context.SaveChangesAsync();

            StatusToast = $"Contact '{ContactInput.FirstName} {ContactInput.LastName}' has been added successfully.";
            StatusToastType = "success";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding contact");
            StatusToast = "An unexpected error occurred while adding the contact.";
            StatusToastType = "danger";
        }

        return RedirectToPage(new { SearchQuery, SelectedCategory });
    }

    public async Task<IActionResult> OnPostEditContactAsync()
    {
        var userName = GetUserIdOrName();

        if (string.IsNullOrEmpty(userName))
        {
            StatusToast = "Failed to update contact. Session expired or user not authenticated.";
            StatusToastType = "danger";
            return RedirectToPage(new { SearchQuery, SelectedCategory });
        }

        if (!ModelState.IsValid)
        {
            StatusToast = "Failed to update contact. Please check your inputs.";
            StatusToastType = "danger";
            return RedirectToPage(new { SearchQuery, SelectedCategory });
        }

        // Re-validate and verify contact ownership
        var existingContact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == ContactInput.Id && c.CreatedBy == userName);

        if (existingContact == null)
        {
            StatusToast = "Contact not found or you do not have permission to edit it.";
            StatusToastType = "danger";
            return RedirectToPage(new { SearchQuery, SelectedCategory });
        }

        // Map values
        existingContact.FirstName = ContactInput.FirstName;
        existingContact.LastName = ContactInput.LastName;
        existingContact.Email = ContactInput.Email;
        existingContact.Phone = ContactInput.Phone;
        existingContact.Company = ContactInput.Company;
        existingContact.Category = ContactInput.Category;
        existingContact.Notes = ContactInput.Notes;

        try
        {
            await _context.SaveChangesAsync();
            StatusToast = $"Contact '{existingContact.FirstName} {existingContact.LastName}' has been updated successfully.";
            StatusToastType = "success";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contact with ID {Id}", ContactInput.Id);
            StatusToast = "An unexpected error occurred while updating the contact.";
            StatusToastType = "danger";
        }

        return RedirectToPage(new { SearchQuery, SelectedCategory });
    }

    public async Task<IActionResult> OnPostDeleteContactAsync(int id)
    {
        var userName = GetUserIdOrName();

        var contactToDelete = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == id && c.CreatedBy == userName);

        if (contactToDelete == null)
        {
            StatusToast = "Contact not found or you do not have permission to delete it.";
            StatusToastType = "danger";
            return RedirectToPage(new { SearchQuery, SelectedCategory });
        }

        try
        {
            _context.Contacts.Remove(contactToDelete);
            await _context.SaveChangesAsync();

            StatusToast = $"Contact '{contactToDelete.FirstName} {contactToDelete.LastName}' has been deleted successfully.";
            StatusToastType = "warning";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact with ID {Id}", id);
            StatusToast = "An unexpected error occurred while deleting the contact.";
            StatusToastType = "danger";
        }

        return RedirectToPage(new { SearchQuery, SelectedCategory });
    }

    private string GetUserIdOrName()
    {
        if (!string.IsNullOrEmpty(User.Identity?.Name))
        {
            return User.Identity.Name;
        }

        var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "email" || c.Type.Contains("emailaddress"));
        if (emailClaim != null && !string.IsNullOrEmpty(emailClaim.Value))
        {
            return emailClaim.Value;
        }

        var subClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.Contains("nameidentifier"));
        if (subClaim != null && !string.IsNullOrEmpty(subClaim.Value))
        {
            return subClaim.Value;
        }

        var userClaim = User.Claims.FirstOrDefault(c => c.Type == "preferred_username");
        if (userClaim != null && !string.IsNullOrEmpty(userClaim.Value))
        {
            return userClaim.Value;
        }

        return string.Empty;
    }
}
