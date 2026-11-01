namespace MedicalApi.Database.Tables;

public class Illness
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<PatientIllness> PatientDiagnoses { get; set; } = [];
}
