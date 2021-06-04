// <copyright file="ITeamMemberCacheHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema.Teams;

    /// <summary>
    /// Interface cache for storing team members information.
    /// </summary>
    public interface ITeamMemberCacheHelper
    {
        /// <summary>
        /// Provide team members information.
        /// </summary>
        /// <param name="turnContext">Provides context for a turn of a bot.</param>
        /// <param name="userId">Describes a user Id.</param>
        /// <param name="teamId">Describes a team Id.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Returns team members information from cache.</returns>
        Task<TeamsChannelAccount> GetMemberInfoAsync(ITurnContext turnContext, string userId, string teamId, CancellationToken cancellationToken);
    }
}
