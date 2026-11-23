using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Teachers.Common;

public static class TeacherDetailsMapper
{
    public static TeacherDetailsDto ToDetailsDto(this Teacher teacher)
    {
        ArgumentNullException.ThrowIfNull(teacher);

        var user = teacher.User
            ?? throw new InvalidOperationException(
                $"User navigation property is null for Teacher: {teacher.Id}");

        var nationality = user.Nationality
            ?? throw new InvalidOperationException(
                $"Nationality navigation property is null for User: {user.Id}");

        var instruments = teacher.TeacherInstruments
            .OrderBy(x => x.Instrument.Name)
            .Select(x => new TeacherInstrumentDetailsDto(
                x.Id,
                x.InstrumentId,
                x.Instrument.Name,
                x.Instrument.EnglishName,
                x.Instrument.ImageUrl,
                x.StartTeachingDate,
                x.TeachingLevel,
                x.DefaultSessionFee,
                x.IsActive,
                x.DefaultBookId,
                x.DefaultBook?.Title,
                x.DefaultBook?.Author))
            .ToList();

        return new TeacherDetailsDto(
            teacher.Id,
            teacher.UserId,
            user.FirstName,
            user.LastName,
            user.Mobile,
            user.Email,
            user.ProfileImageUrl,
            user.BirthDate,
            user.Gender,
            user.NationalIdentificationNumber,
            user.Address,
            user.Bio,
            nationality.PersianCountryName,
            teacher.PersonnelCode,
            teacher.EmploymentDate,
            teacher.Education,
            teacher.ExperienceYears,
            teacher.Biography,
            teacher.IsVisibleOnWebsite,
            teacher.Status,
            instruments);
    }
}
