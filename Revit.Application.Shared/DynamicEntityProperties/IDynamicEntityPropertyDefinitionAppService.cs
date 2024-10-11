using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.DynamicEntityProperties
{
    public interface IDynamicEntityPropertyDefinitionAppService
    {
        Task<List<string>> GetAllAllowedInputTypeNames();

        Task<List<string>> GetAllEntities();
    }
}
