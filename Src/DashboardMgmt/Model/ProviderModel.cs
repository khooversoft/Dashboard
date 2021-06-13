using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;

namespace DashboardMgmt.Model
{
    public class ProviderModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Provider is too long")]
        public string Provider { get; set; } = null!;

        public bool Show { get; set; } = true;
    }

    public static class ProviderModelExtensions
    {
        public static ProviderModel? ConvertTo(this ProviderRecord? subject)
        {
            if (subject == null) return null;

            return new ProviderModel
            {
                Provider = subject.Provider,
                Show = subject.Show,
            };
        }
    }
}
