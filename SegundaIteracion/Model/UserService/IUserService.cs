using System;

using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;

namespace Es.Udc.DotNet.MiniPortal.Model.UserService
{
    public interface IUserService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }
        [Inject]
        IUserGroup1Dao GroupDao { set; }

        IRecommendationDao RecommendationDao { set; }
        [Inject]
        IEventDao EventDao { set; }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="clearPassword">The clear password.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long RegisterUser(String loginName, String clearPassword,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Logins the specified login name.
        /// </summary>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="password">The password.</param>
        /// <param name="passwordIsEncrypted">if set to <c>true</c> [password is
        /// encrypted].</param>
        /// <returns>LoginResult</returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>        
        [Transactional]
        LoginResult Login(String loginName, String password,
            Boolean passwordIsEncrypted);

        /// <summary>
        /// Finds the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <returns>The user profile details</returns> 
        /// <exception cref="InstanceNotFoundException"/>  
        [Transactional]
        UserProfileDetails FindUserProfileDetails(long userProfileId);

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userProfileId">The user profile id.</param>
        /// <param name="oldClearPassword">The old clear password.</param>
        /// <param name="newClearPassword">The new clear password.</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        void ChangePassword(long userProfileId, String oldClearPassword,
            String newClearPassword);

        /// <summary>
        /// Adds user to the group
        /// </summary>
        /// <param userId="userId">The user's id.</param>
        /// <param groupId="GroupId">The Group Id</param>
        /// <returns>The Group</returns> 
        /// <exception cref="InstanceNotFoundException" />
        /// <exception cref="DuplicateInstanceException"/> 
        [Transactional]
        UserGroup JoinGroup(long userId, long groupId);

        /// <summary>
        /// One user unjoins the group
        /// </summary>
        /// <param userId="userId">The user's id.</param>
        /// <param groupId="GroupId">The Group Id</param>
        /// <returns>The Group</returns> 
        /// <exception cref="InstanceNotFoundException" />
        /// <exception cref="DuplicateInstanceException"/> 
        [Transactional]
        UserGroup UnJoinGroup(long userId, long groupId);

        /// <summary>
        /// Shows joined groups by one user
        /// </summary>
        /// <param userId="UserId">The user's id.</param>
        /// <returns>The List of GroupsDto</returns> 
        /// <exception cref="InstanceNotFoundException" />
        [Transactional]
        ICollection<UserGroupDto> FindGroupsByUserId(long userId);

        /// <summary>
        /// Finds group by name.
        /// </summary>
        /// <param name="Name">The group name.</param>
        /// <returns>The groupDto</returns> 
        /// <exception cref="InstanceNotFoundException"/>  
        [Transactional]
        UserGroupDto FindGroupByName(string name);

        /// <sumary>
        /// List all groups
        /// </sumary>
        /// <returns>All the groupsDto</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        ICollection<UserGroupDto> FindAllGroups();

        /// <summary>
        /// Find user by Login Name
        /// </summary>
        /// <returns>One user</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]   UserProfile FindUserByLoginName(string login);
     

        /// <sumary>
        /// List all groups
        /// </sumary>
        /// /// <param name="Name">The group name.</param>
        /// /// <param description="Description">The group description.</param>
        /// /// <param userId="UserId">The user's id.</param>
        /// <returns>Add new groups</returns>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        UserGroup AddGroup(string name, string description, long userId);

        /// <summary>
        /// Add Recommendation to an Event
        /// </summary>
        /// <param eventId="EventId">The Event to recommend</param>
        /// <param groups="Groups">The Groups where you are recommending</param>
        /// <param userId="UserId">The User who recommends</param>
        /// <param description="Description">The Description of the recommendation</param>
        /// <exception cref="InstanceNotFoundException" />
        [Transactional]
        ICollection<Recommendation> AddRecommendation(long eventId, ICollection<long> groups, long userId, string description);

        /// <summary>
        /// Visualize the group recommendations to an Event??????
        /// </summary>
        /// <param eventId="EventId">The Event to recommend</param>
        /// <param groupId="GroupId">The Group where you are recommending</param>
        /// <param userId="UserId">The User that wants to search the recommendations</param> ??? SOBRA AQUI
        /// <param startIndex="StartIndex">integer</param>
        /// <param count="Count">integer</param>
        /// <exception cref="InstanceNotFoundException" />
        [Transactional]
        ICollection<Recommendation> FindGroupRecommendations(long groupId, long userId, int startIndex, int count);


    }

}