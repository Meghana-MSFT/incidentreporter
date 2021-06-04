// <copyright file="TicketHelper.cs" company="Microsoft">
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
    public class TicketHelper : ITicketHelper
    {
        /// <summary>
        /// Helper that handles the card configuration.
        /// </summary>
        private readonly ICardHelper cardHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketHelper"/> class.
        /// </summary>
        /// <param name="cardHelper">Microsoft Application credentials for Bot/ME.</param>
        public TicketHelper(ICardHelper cardHelper)
        {
            this.cardHelper = cardHelper;
        }

        /// <summary>
        /// Validates user entered ticket details.
        /// </summary>
        /// <param name="updatedTicketDetail">Ticket details entered by the user.</param>
        /// <returns>Returns success/failure depending on whether validation succeeds.</returns>
        public bool ValidateRequestDetail(TicketDetail updatedTicketDetail)
        {
            return updatedTicketDetail != null
                && !string.IsNullOrWhiteSpace(updatedTicketDetail.Title)
                && !string.IsNullOrWhiteSpace(updatedTicketDetail.Description);
        }

        /// <summary>
        /// Update the ticket from the edited request.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetail">Ticket details entered by user.</param>
        /// <param name="taskModuleResponseValues">Edited response details from task module.</param>
        /// <returns>TicketDetail object.</returns>
        public TicketDetail GetUpdatedTicketDetails(ITurnContext<IInvokeActivity> turnContext, TicketDetail ticketDetail, TicketDetail taskModuleResponseValues)
        {
            turnContext = turnContext ?? throw new ArgumentNullException(nameof(turnContext));
            ticketDetail = ticketDetail ?? throw new ArgumentNullException(nameof(ticketDetail));
            if (ticketDetail.IssueOccurredOn == DateTimeOffset.MinValue || taskModuleResponseValues?.IssueOccurredOn == DateTimeOffset.MinValue)
            {
                ticketDetail.IssueOccurredOn = this.ConvertToDateTimeoffset(DateTime.Now, turnContext.Activity.LocalTimestamp.Value.Offset);
            }
            else
            {
                ticketDetail.IssueOccurredOn = this.ConvertToDateTimeoffset(taskModuleResponseValues.IssueOccurredOn, turnContext.Activity.LocalTimestamp.Value.Offset);
            }

            ticketDetail.Description = taskModuleResponseValues?.Description;
            ticketDetail.Title = taskModuleResponseValues.Title;
            ticketDetail.Severity = (int)(TicketSeverity)Enum.Parse(typeof(TicketSeverity), taskModuleResponseValues.RequestType ?? TicketSeverity.Normal.ToString());
            ticketDetail.LastModifiedOn = this.ConvertToDateTimeoffset(DateTime.Now, turnContext.Activity.LocalTimestamp.Value.Offset);
            ticketDetail.LastModifiedByName = turnContext.Activity.From.Name;
            ticketDetail.LastModifiedByObjectId = turnContext.Activity.From.AadObjectId;
            ticketDetail.RequestType = taskModuleResponseValues.RequestType ?? TicketSeverity.Normal.ToString();
            return ticketDetail;
        }

        /// <summary>
        /// Create a new ticket from the input.
        /// </summary>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="ticketDetail">Ticket details from requested user.</param>
        /// <param name="ticketAdditionalDetails">Additional ticket details.</param>
        /// <param name="cardId">Card template id.</param>
        /// <param name="member"> User details who is currently having conversation.</param>
        /// <returns>TicketDetail object.</returns>
        public TicketDetail GetNewTicketDetails(ITurnContext<IMessageActivity> turnContext, TicketDetail ticketDetail, string ticketAdditionalDetails, string cardId, TeamsChannelAccount member)
        {
            turnContext = turnContext ?? throw new ArgumentNullException(nameof(turnContext));
            ticketDetail = ticketDetail ?? throw new ArgumentNullException(nameof(ticketDetail));

            ticketDetail.CreatedOn = this.ConvertToDateTimeoffset(DateTime.Now, turnContext.Activity.LocalTimestamp.Value.Offset);
            if (ticketDetail.IssueOccurredOn == DateTimeOffset.MinValue)
            {
                ticketDetail.IssueOccurredOn = this.ConvertToDateTimeoffset(DateTime.Now, turnContext.Activity.LocalTimestamp.Value.Offset);
            }
            else
            {
                ticketDetail.IssueOccurredOn = this.ConvertToDateTimeoffset(ticketDetail.IssueOccurredOn, turnContext.Activity.LocalTimestamp.Value.Offset);
            }

            ticketDetail.CreatedByObjectId = turnContext.Activity.From.AadObjectId;
            ticketDetail.CreatedByUserPrincipalName = member?.UserPrincipalName;
            ticketDetail.RequesterName = member.Name;
            ticketDetail.RequesterConversationId = turnContext.Activity.Conversation.Id;
            ticketDetail.RequesterTicketActivityId = turnContext.Activity.ReplyToId;
            ticketDetail.SmeConversationId = null;
            ticketDetail.SmeTicketActivityId = null;
            ticketDetail.TicketStatus = (int)TicketState.Unassigned;
            ticketDetail.Severity = (int)(TicketSeverity)Enum.Parse(typeof(TicketSeverity), ticketDetail.RequestType ?? TicketSeverity.Normal.ToString());
            ticketDetail.AdditionalProperties = this.cardHelper.ValidateAdditionalTicketDetails(ticketAdditionalDetails, turnContext.Activity.LocalTimestamp.Value.Offset);
            ticketDetail.CardId = cardId;
            ticketDetail.AssignedToName = string.Empty;
            ticketDetail.AssignedToObjectId = string.Empty;

            return ticketDetail;
        }

        /// <summary>
        /// Convert date time to local times tamp offset.
        /// </summary>
        /// <param name="datetime">input date time.</param>
        /// <param name="timeSpan">Local time stamp.</param>
        /// <returns>Local date time offset.</returns>
        public DateTimeOffset ConvertToDateTimeoffset(DateTimeOffset datetime, TimeSpan timeSpan)
        {
            if (datetime != DateTimeOffset.MinValue)
            {
                return new DateTimeOffset(
                       datetime.Year,
                       datetime.Month,
                       datetime.Day,
                       datetime.Hour,
                       datetime.Minute,
                       datetime.Second,
                       timeSpan).ToUniversalTime();
            }
            else
            {
                return datetime;
            }
        }
    }
}