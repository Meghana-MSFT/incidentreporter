// <copyright file="ITicketHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Microsoft.Bot.Schema.Teams;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;

    /// <summary>
    /// Handles the ticket activities.
    /// </summary>
    public interface ITicketHelper
    {
        /// <summary>
        /// Validates user entered ticket details.
        /// </summary>
        /// <param name="updatedTicketDetail">Ticket details entered by the user.</param>
        /// <returns>Returns success/failure depending on whether validation succeeds.</returns>
        bool ValidateRequestDetail(TicketDetail updatedTicketDetail);

        /// <summary>
        /// Update the ticket from the edited request.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetail">Ticket details entered by user.</param>
        /// <param name="taskModuleResponseValues">Edited response details from task module.</param>
        /// <returns>TicketDetail object.</returns>
        TicketDetail GetUpdatedTicketDetails(ITurnContext<IInvokeActivity> turnContext, TicketDetail ticketDetail, TicketDetail taskModuleResponseValues);

        /// <summary>
        /// Create a new ticket from the input.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetail">Ticket details from requested user.</param>
        /// <param name="ticketAdditionalDetails">Additional ticket details.</param>
        /// <param name="cardId">Card template id.</param>
        /// <param name="member"> User details who is currently having conversation.</param>
        /// <returns>TicketDetail object.</returns>
        TicketDetail GetNewTicketDetails(ITurnContext<IMessageActivity> turnContext, TicketDetail ticketDetail, string ticketAdditionalDetails, string cardId, TeamsChannelAccount member);

        /// <summary>
        /// Convert date time to local times tamp offset.
        /// </summary>
        /// <param name="datetime">input date time.</param>
        /// <param name="timeSpan">Local time stamp.</param>
        /// <returns>Local date time offset.</returns>
        DateTimeOffset ConvertToDateTimeoffset(DateTimeOffset datetime, TimeSpan timeSpan);
    }
}
