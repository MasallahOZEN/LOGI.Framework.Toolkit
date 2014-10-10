using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGI.Framework.Toolkit.Foundation.Membership
{
    /// <summary>
    /// Represents an online user info
    /// </summary>
    public interface IOnlineUserInfo
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        Guid OnlineUserGuid { get; }

        /// <summary>
        /// Gets or sets the associated customer identifier (if he exists)
        /// </summary>
        int? AssociatedCustomerId { get;}

        /// <summary>
        /// Gets or sets the IP Address
        /// </summary>
        string IPAddress { get;}

        /// <summary>
        /// Gets or sets the last page visited
        /// </summary>
        string LastPageVisited { get; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        DateTime CreatedOn { get; }

        /// <summary>
        /// Gets or sets the last visit date and time
        /// </summary>
        DateTime LastVisit { get; }
    }
}
