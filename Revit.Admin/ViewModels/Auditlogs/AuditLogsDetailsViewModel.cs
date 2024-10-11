using Revit.Auditing.Dto;
using Revit.Shared; 
using Prism.Services.Dialogs;

namespace Revit.Admin.ViewModels
{
    public class AuditLogsDetailsViewModel : HostDialogViewModel
    {
        private AuditLogListDto auditLog;

        public AuditLogListDto AuditLog
        {
            get { return auditLog; }
            set { auditLog = value; OnPropertyChanged(); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                AuditLog = parameters.GetValue<AuditLogListDto>("Value");
            }
        }
    }
}
