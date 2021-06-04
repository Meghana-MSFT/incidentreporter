// <copyright file="ISearchHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Schema.Teams;
    using Microsoft.Extensions.Localization;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Providers;

    /// <summary>
    /// Interface that handles the search activities for messaging extension.
    /// </summary>
    public interface ISearchHelper
    {
        /// <summary>
        /// Get the value of the searchText parameter in the messaging extension query.
        /// </summary>
        /// <param name="query">Contains messaging extension query keywords.</param>
        /// <returns>A value of the searchText parameter.</returns>
        string GetSearchQueryString(MessagingExtensionQuery query);

        /// <summary>
        /// Get the results from Azure search service and populate the result (card + preview).
        /// </summary>
        /// <param name="query">Query which the user had typed in message extension search.</param>
        /// <param name="commandId">Command id to determine which tab in message extension has been invoked.</param>
        /// <param name="count">Count for pagination.</param>
        /// <param name="skip">Skip for pagination.</param>
        /// <param name="searchService">Search service.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="requestorId">Requester id of the user to get specific tickets.</param>
        /// <param name="onCallSMEUsers">OncallSMEUsers to give support from group-chat or on-call.</param>
        /// <returns><see cref="Task"/> Returns MessagingExtensionResult which will be used for providing the card.</returns>
        Task<MessagingExtensionResult> GetSearchResultAsync(
            string query,
            string commandId,
            int? count,
            int? skip,
            ITicketSearchService searchService,
            IStringLocalizer<Strings> localizer,
            string requestorId = "",
            string onCallSMEUsers = "");

        /// <summary>
        /// Get result for messaging extension tab.
        /// </summary>
        /// <param name="searchServiceResults">List of tickets from Azure search service.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="commandId">Command id to determine which tab in message extension has been invoked.</param>
        /// <param name="onCallSMEUsers">OncallSMEUsers to give support from group-chat or on-call.</param>
        /// <returns><see cref="Task"/> Returns MessagingExtensionResult which will be shown in messaging extension tab.</returns>
        MessagingExtensionResult GetMessagingExtensionResult(
            IList<TicketDetail> searchServiceResults,
            IStringLocalizer<Strings> localizer,
            string commandId = "",
            string onCallSMEUsers = "");
    }
}
