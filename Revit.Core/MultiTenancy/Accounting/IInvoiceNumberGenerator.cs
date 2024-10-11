using System.Threading.Tasks;
using Abp.Dependency;

namespace AppFramework.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}