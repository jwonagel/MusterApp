// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanToVisibilityConverter.cs" company="Gorba AG">
//   Copyright © 2011-2015 Gorba AG. All rights reserved.
// </copyright>
// <summary>
//   Defines the BooleanToVisibilityExtendedConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MusterApp.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    using MusterApp.Models;

    using Telerik.Windows.Diagrams.Core;

    /// <summary>
    /// Converts a <see cref="Boolean"/> value to <see cref="Visibility"/> equivalent.
    /// By default, <c>true</c> is converted to <see cref="Visibility.Visible"/>
    /// and <c>false</c> to <see cref="Visibility.Hidden"/>.
    /// </summary>
    public class StatisticsConverter : IValueConverter
    {
        private const Visibility DefaultFalseVisibility = Visibility.Hidden;

        private const Visibility DefaultTrueVisibility = Visibility.Visible;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusterApp.Converters.BooleanToVisibilityExtendedConverter"/> class.
        /// </summary>
        public StatisticsConverter()
        {
            this.FalseVisibility = DefaultFalseVisibility;
            this.TrueVisibility = DefaultTrueVisibility;
        }

        /// <summary>
        /// Gets or sets the visibility for <c>false</c> values.
        /// </summary>
        public Visibility FalseVisibility { get; set; }

        /// <summary>
        /// Gets or sets the visibility for <c>true</c> values.
        /// </summary>
        public Visibility TrueVisibility { get; set; }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var pod = (pod)value;
                if (pod == null)
                {
                    return "Please select a Pod";
                }

                if (parameter == null)
                {
                    return string.Empty;
                }

                if ((Params)parameter != Params.Obsolete)
                {
                    return string.Empty;
                }

                var obsolete = pod.location;

                return (from location in obsolete
                        from position in location.position_abrechnung
                        where position.abrechnung == null
                        select (int)position.preis).Sum();
            }
            catch (InvalidCastException exception)
            {
                throw new NotSupportedException("Only boolean values are supported by this converter", exception);
            }
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
