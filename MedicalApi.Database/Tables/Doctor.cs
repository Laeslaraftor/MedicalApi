namespace MedicalApi.Database.Tables;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public ICollection<PatientIllness> Diagnoses { get; set; } = [];
}
