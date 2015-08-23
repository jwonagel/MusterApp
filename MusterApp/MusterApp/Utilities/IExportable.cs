// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExportable.cs" company="Gorba AG">
//   Copyright © 2011-2015 Gorba AG. All rights reserved.
// </copyright>
// <summary>
//   The Exportable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceReportWizard.Utility
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Exportable interface.
    /// </summary>
    public interface IExportable
    {
        /// <summary>
        /// The value tuples.
        /// </summary>
        /// <returns>
        /// The <see cref="Tuple"/>.
        /// </returns>
        IEnumerable<Tuple<string, string>> ValueTuples();
    }
}
