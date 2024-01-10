namespace SmartHotel.Core;

public interface IEntityBase
{
    int Id { get; set; }

    bool IsDeleted { get; set; }

    DateTimeOffset CreatedOn { get; set; }

    string CreatedBy { get; set; }

    DateTimeOffset UpdatedOn { get; set; }

    string UpdatedBy { get; set; }
}
