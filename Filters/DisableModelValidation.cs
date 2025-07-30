using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeesWebAPI.Filters
{
    public class DisableModelValidation : Attribute, IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            action.Filters.Where(f => f is ModelStateInvalidFilter)
                          .ToList()
                          .ForEach(f => action.Filters.Remove(f));
        }
    }
}
