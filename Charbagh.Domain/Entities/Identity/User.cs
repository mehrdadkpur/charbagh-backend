using Charbagh.Domain.Entities.CMS;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Domain.Entities.People;

namespace Charbagh.Domain.Entities.Identity;


public class User : BaseEntity
{

    #region Personal Information

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Mobile { get; private set; } = null!;

    public string? Email { get; private set; }

    public DateOnly? BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public string? Address { get; private set; }

    public string? Bio { get; private set; }

    public string? ProfileImageUrl { get; private set; }

    #endregion

    #region Authentication

    public byte[]? PasswordHash { get; private set; }

    public byte[]? PasswordSalt { get; private set; }

    public string SecurityStamp { get; private set; } = Guid.NewGuid().ToString();

    public bool MobileConfirmed { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public DateTime? LastLoginAt { get; private set; }

    #endregion

    #region Status

    public UserStatus Status { get; private set; } = UserStatus.Pending;

    #endregion

    #region Nationality 

    public Guid NationalityId { get; private set; }
    public string NationalIdentificationNumber { get; private set; } = null!;
    public Nationality Nationality { get; private set; } = null!;

    #endregion

    #region Domain Profiles
    public Teacher? Teacher { get; private set; }

    public Student? Student { get; private set; }

    public Parent? Parent { get; private set; }

    public Staff? Staff { get; private set; }

    #endregion

    #region Authorization

    public ICollection<UserBranch> UserBranches { get; private set; } = new List<UserBranch>();

    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    public ICollection<LoginHistory> LoginHistories { get; private set; } = new List<LoginHistory>();

    #endregion

    #region Content

    public ICollection<Blog> Blogs { get; private set; } = new List<Blog>();

    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

    public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    #endregion

    public string FullName => $"{FirstName} {LastName}";

    private User()
    {
    }
    private User(
        string firstName, 
        string lastName, 
        string mobile, 
        string? email, 
        Guid nationalityId, 
        string nationalIdentificationNumber, 
        Gender gender , 
        string? address, 
        DateOnly? birthDate,
        string? bio,
        string? profileImageUrl
        ) 
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Mobile = mobile.Trim();
        Email = email?.Trim();
        NationalityId = nationalityId;
        NationalIdentificationNumber = nationalIdentificationNumber.Trim();
        Gender = gender;
        Address = address?.Trim();
        BirthDate = birthDate;
        Bio = bio?.Trim();
        ProfileImageUrl = profileImageUrl?.Trim();
        Status = UserStatus.Pending;
    }

    #region Factory 
    public static User Create( 
        string firstName, 
        string lastName, 
        string mobile, 
        string? email ,
        Guid nationalityId,
        string nationalIdentificationNumber, 
        Gender gender ,
        string? address,
        DateOnly? birthDate,
        string? bio,
        string? profileImageUrl
        ) 
    { 
        var user = new User( 
            firstName, 
            lastName, 
            mobile, 
            email ,
            nationalityId,
            nationalIdentificationNumber, 
            gender , 
            address , 
            birthDate , 
            bio , 
            profileImageUrl 
            );

        user.MarkAsCreated(); 
        return user; 
    }

    public void Update(
     string firstName,
     string lastName,
     string mobile,
     string? email,
     Guid nationalityId,
     string nationalIdentificationNumber,
     Gender gender,
     string? address,
     DateOnly? birthDate,
     string? bio,
     string? profileImageUrl)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Mobile = mobile.Trim();

        Email = email?.Trim();
        NationalityId = nationalityId;
        NationalIdentificationNumber = nationalIdentificationNumber.Trim();

        Gender = gender;

        Address = address?.Trim();
        BirthDate = birthDate;
        Bio = bio?.Trim();
        ProfileImageUrl = profileImageUrl?.Trim();

        MarkAsUpdated();
    }
    #endregion

    #region Personal Information 
    public void UpdatePersonalInformation( string firstName, string lastName, string mobile, Gender gender, string? email = null, string? nationalIdentificationNumber = null, DateOnly? birthDate = null, string? address = null, string? bio = null, string? profileImageUrl = null) 
    { 
        FirstName = firstName.Trim(); 
        LastName = lastName.Trim(); 
        Mobile = mobile.Trim(); 
        Gender = gender; 
        Email = email?.Trim();
        NationalIdentificationNumber = nationalIdentificationNumber?.Trim(); 
        BirthDate = birthDate; 
        Address = address?.Trim(); 
        Bio = bio?.Trim(); 
        ProfileImageUrl = profileImageUrl?.Trim();

        MarkAsUpdated(); 
    }
    #endregion

    #region Authentication 
    public void SetPassword(byte[] passwordHash, byte[] passwordSalt) 
    { 
        PasswordHash = passwordHash; 
        PasswordSalt = passwordSalt; 
        SecurityStamp = Guid.NewGuid().ToString(); 
        MarkAsUpdated(); 
    } 
    public void ConfirmMobile() 
    { 
        MobileConfirmed = true; 
        MarkAsUpdated(); 
    } 
    public void ConfirmEmail() 
    { 
        EmailConfirmed = true; 
        MarkAsUpdated(); 
    } 
    public void RecordLogin() 
    { 
        LastLoginAt = DateTime.UtcNow; MarkAsUpdated(); 
    } 
    public void RegenerateSecurityStamp() 
    { 
        SecurityStamp = Guid.NewGuid().ToString(); 
        MarkAsUpdated();
    }

    #endregion

    #region Status 
    public void Activate() 
    { 
        if(Status == UserStatus.Blocked)
        {
            throw new InvalidOperationException("Blocked user cannot be activated directly.");
        }
        Status = UserStatus.Active; 
        MarkAsUpdated();
    }
    public void Suspend()
    {
        if (Status == UserStatus.Blocked)
        {
            throw new InvalidOperationException( "Blocked user cannot be suspended.");
        }
        Status = UserStatus.Suspended;
        MarkAsUpdated();
    }
    public void Block()
    {
        if (Status == UserStatus.Blocked)
            return;

        Status = UserStatus.Blocked;
        MarkAsUpdated();
    }
    public void Unblock()
    {
        if (Status != UserStatus.Blocked)
        {
            throw new InvalidOperationException("Only a blocked user can be unblocked.");
        }

        Status = UserStatus.Active;
        MarkAsUpdated();
    }
    public void SetPending() 
    { 
        Status = UserStatus.Pending; 
        MarkAsUpdated(); 
    } 
    #endregion
}