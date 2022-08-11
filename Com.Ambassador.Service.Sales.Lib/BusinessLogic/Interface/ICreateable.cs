using System.Threading.Tasks;

namespace Com.Ambassador.Service.Purchasing.Lib.Interfaces
{
    public interface ICreateable
    {
        Task<int> Create(object model);
    }
}
