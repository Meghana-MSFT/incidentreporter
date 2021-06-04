// <copyright file="IEditRequestCard.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Cards
{
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Localization;
    using Microsoft.Teams.Apps.RemoteSupport.Common.Models;

    /// <summary>
    /// Interface that holds card for Edit request.
    /// </summary>
    public interface IEditRequestCard
    {
        /// <summary>
        /// Gets Edit card for task module.
        /// </summary>
        /// <param name="ticketDetail">Ticket details from user.</param>
        /// <param name="cardConfiguration">Card configuration.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="existingTicketDetail">Existing ticket details.</param>
        /// <returns>Returns an attachment of edit card.</returns>
        Attachment GetEditRequestCard(TicketDetail ticketDetail, CardConfigurationEntity cardConfiguration, IStringLocalizer<Strings> localizer, TicketDetail existingTicketDetail = null);

        /// <summary>
        /// Construct the card to render error message text to task module.
        /// </summary>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <returns>Card attachment.</returns>
        Attachment GetClosedErrorCard(IStringLocalizer<Strings> localizer);
    }
}
