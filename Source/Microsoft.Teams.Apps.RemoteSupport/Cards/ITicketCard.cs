// <copyright file="ITicketCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Cards
{
    using System.Collections.Generic;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Localization;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;

    /// <summary>
    /// Interface that provides adaptive cards for creating and editing new ticket information.
    /// </summary>
    public interface ITicketCard
    {
        /// <summary>
        /// Get the create new ticket card.
        /// </summary>
        /// <param name="cardConfiguration">Card configuration.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="showValidationMessage">Represents whether to show validation message or not.</param>
        /// <param name="ticketDetail"> Information of the ticket which is being created.</param>
        /// <returns>Returns an attachment of new ticket.</returns>
        Attachment GetNewTicketCard(CardConfigurationEntity cardConfiguration, IStringLocalizer<Strings> localizer, bool showValidationMessage = false, TicketDetail ticketDetail = null);

        /// <summary>
        /// Card to show ticket details in 1:1 chat with bot after submitting request details.
        /// </summary>
        /// <param name="cardElementMapping">Represents Adaptive card item element {Id, display name} mapping.</param>
        /// <param name="ticketDetail">New ticket values entered by user.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="isEdited">flag that sets when card is edited.</param>
        /// <returns>An attachment with ticket details.</returns>
        Attachment GetTicketDetailsForPersonalChatCard(Dictionary<string, string> cardElementMapping, TicketDetail ticketDetail, IStringLocalizer<Strings> localizer, bool isEdited = false);
    }
}
