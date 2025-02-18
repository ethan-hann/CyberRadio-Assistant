using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AetherUtils.Core.Attributes;

namespace RadioExt_Helper.config
{
    /// <summary>
    /// Represents a forbidden keyword that should not be present in a path.
    /// </summary>
    public class ForbiddenKeyword(string group, string keyword, bool isForbidden)
    {
        /// <summary>
        /// The group that the keyword belongs to.
        /// </summary>
        [Config("group")]
        public string Group { get; set; } = group;

        /// <summary>
        /// The keyword that is forbidden.
        /// </summary>
        [Config("keyword")]
        public string Keyword { get; set; } = keyword;

        /// <summary>
        /// Indicates if the keyword is forbidden.
        /// </summary>
        [Config("isForbidden")]
        public bool IsForbidden { get; set; } = isForbidden;

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public ForbiddenKeyword() : this(string.Empty, string.Empty, false) { }
    }
}
