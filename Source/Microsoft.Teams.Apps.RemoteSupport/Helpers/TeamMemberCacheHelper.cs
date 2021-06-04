// <copyright file="TeamMemberCacheHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.RemoteSupport.Helpers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Teams;
    using Microsoft.Bot.Schema.Teams;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Teams.Apps.RemoteSupport.Common;

    /// <summary>
    /// Implements team member cache.
    /// </summary>
    public class TeamMemberCacheHelper : ITeamMemberCacheHelper
    {
        /// <summary>
        /// Cache for storing teamMembers information.
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// Sets the team members cache key.
        /// </summary>
        private TimeSpan cacheDuration = TimeSpan.FromDays(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberCacheHelper"/> class.
        /// </summary>
        /// <param name="memoryCache">MemoryCache instance for caching authorization result.</param>
        public TeamMemberCacheHelper(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Provide team members information.
        /// </summary>
        /// <param name="turnContext">Provides context for a turn of a bot.</param>
        /// <param name="userId">Describes a user Id.</param>
        /// <param name="teamId">Describes a team Id.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Returns team members information from cache.</returns>
        public async Task<TeamsChannelAccount> GetMemberInfoAsync(ITurnContext turnContext, string userId, string teamId, CancellationToken cancellationToken)
        {
            bool isCacheEntryExists = this.memoryCache.TryGetValue(Constants.ExpertCollectionCacheKey + userId, out TeamsChannelAccount memberInformation);

            if (!isCacheEntryExists)
            {
                if (teamId != null)
                {
                    memberInformation = await TeamsInfo.GetTeamMemberAsync(turnContext, userId, teamId);
                }
                else
                {
                    memberInformation = await TeamsInfo.GetMemberAsync(turnContext, userId, cancellationToken);
                }
            }

            if (memberInformation != null)
            {
                this.memoryCache.Set(Constants.ExpertCollectionCacheKey + userId, memberInformation, this.cacheDuration);
            }

            return memberInformation;
        }
    }
}
