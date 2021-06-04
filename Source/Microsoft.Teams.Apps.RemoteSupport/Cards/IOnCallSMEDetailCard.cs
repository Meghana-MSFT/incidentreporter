// <copyright file="IOnCallSMEDetailCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Cards
{
    using System.Collections.Generic;
    using AdaptiveCards;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Localization;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;

    /// <summary>
    ///  Interface that provides adaptive cards for managing on call support team details and viewing on call experts update history.
    /// </summary>
    public interface IOnCallSMEDetailCard
    {
        /// <summary>
        /// Gets on call SME detail card.
        /// </summary>
        /// <param name="onCallSupportDetails"> Collection of last 10 modified on call support team details.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>Returns an attachment of card showing on call support details.</returns>
        Attachment GetOnCallSMEDetailCard(IEnumerable<OnCallSupportDetail> onCallSupportDetails, IStringLocalizer<Strings> localizer);

        /// <summary>
        /// Card to show confirmation on selecting withdraw action.
        /// </summary>
        /// <param name="onCallSupportDetails"> Collection of class containing details of on call support team.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>An attachment with confirmation(yes/no)card.</returns>
        AdaptiveCard OnCallSMEUpdateHistoryCard(IEnumerable<OnCallSupportDetail> onCallSupportDetails, IStringLocalizer<Strings> localizer);
    }
}
