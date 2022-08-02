

using Business.Core.Contracts;

namespace Business.Core.Interfaces
{
    public interface ISeqServices
    {
        Task<string> ApiGenSeqType(string type);
    }
}
