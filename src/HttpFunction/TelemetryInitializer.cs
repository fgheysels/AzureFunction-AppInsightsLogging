using System.Diagnostics;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace HttpFunction
{
    public class TelemetryInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Initializes properties of the specified <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry" /> object.
        /// </summary>
        public void Initialize(ITelemetry telemetry)
        {
            if (Activity.Current == null)
            {
                return;
            }

            telemetry.Context.Operation.Id = Activity.Current.RootId;
            telemetry.Context.Operation.ParentId = Activity.Current.ParentId;
        }
    }
}