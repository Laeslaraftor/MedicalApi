namespace MedicalApi.Database.Tables;

public class PatientIllness
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public int DiseaseId { get; set; }
    public Illness Disease { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
}
