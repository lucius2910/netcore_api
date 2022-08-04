namespace Business.Core.Contracts
{
    public class MachineResponse
    {
        public Guid id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? department_cd { get; set; }
        public string? supplier_cd { get; set; }
        public string? admin_cd { get; set; }
        public string? mold_cd { get; set; }
        public string? type { get; set; }
        public int? size1 { get; set; }
        public int? size2 { get; set; }
        public int? size3 { get; set; }
        public string? color { get; set; }
        public string? effective_date { get; set; }
        public string? remarks { get; set; }
    }
}
