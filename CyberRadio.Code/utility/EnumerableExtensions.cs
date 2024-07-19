using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioExt_Helper.models;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Generates a <see cref="BindingList{T}"/> from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of object contained in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to get a <see cref="BindingList{T}"/> for.</param>
        /// <returns>A <see cref="BindingList{T}"/> representing the <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> was <c>null</c>.</exception>
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new BindingList<T>(new List<T>(source));
        }
    }
}
