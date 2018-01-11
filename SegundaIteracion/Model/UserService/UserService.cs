using System;
using Es.Udc.DotNet.MiniPortal.Model.UserProfileDao;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Util;
using Es.Udc.DotNet.MiniPortal.Model.UserService.Exceptions;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System.Collections.Generic;
using Es.Udc.DotNet.MiniPortal.Model.UserGroup1Dao;
using System.Collections;
using System.Web.UI.MobileControls;
using Es.Udc.DotNet.MiniPortal.Model.RecommendationDao;
using Es.Udc.DotNet.MiniPortal.Model.EventDao;

namespace Es.Udc.DotNet.MiniPortal.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public IUserGroup1Dao GroupDao { private get; set; }
        [Inject]
        public IRecommendationDao RecommendationDao { private get; set; }
        [Inject]
        public IEventDao EventDao { private get; set; }

        #region IUserService Members

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails)
        {

            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);

            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.language = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;

                UserProfileDao.Create(userProfile);

                return userProfile.usrId;

            }

        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>        
        [Transactional]
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile =
                UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.usrId, userProfile.firstName,
                storedPassword, userProfile.language, userProfile.country);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
                PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }


        [Transactional]
        public ICollection<UserGroupDto> FindGroupsByUserId(long userId)
        {
            UserProfile user = UserProfileDao.Find(userId);
            ICollection<UserGroup> groups = user.UserGroups;
            ICollection<UserGroupDto> groupsDto = new List<UserGroupDto>();
            foreach (UserGroup group in groups)
            {
                groupsDto.Add(new UserGroupDto(group));
            }
            return groupsDto;
        }

        [Transactional]
        public UserGroup JoinGroup(long userId, long groupId)
        {
            UserGroup group = GroupDao.Find(groupId);
            UserProfile user = UserProfileDao.Find(userId);
            group.UserProfiles.Add(user);
            GroupDao.Update(group);
            return group;
        }

        [Transactional]
        public UserGroup UnJoinGroup(long userId, long groupId)
        {
            UserGroup group = GroupDao.Find(groupId);
            UserProfile user = UserProfileDao.Find(userId);
            group.UserProfiles.Remove(user);
            GroupDao.Update(group);
            return group;
        }

        [Transactional]
        public UserGroupDto FindGroupByName(string name)
        {
            UserGroup group = GroupDao.FindByName(name);
            UserGroupDto groupDto = new UserGroupDto(group);

            return groupDto;
        }

        [Transactional]
        public ICollection<UserGroupDto> FindAllGroups()
        {
            ICollection<UserGroup> groups = GroupDao.GetAllElements();
            ICollection<UserGroupDto> groupsDto = new List<UserGroupDto>();
            foreach (UserGroup group in groups)
            {
                groupsDto.Add(new UserGroupDto(group));
            }
            return groupsDto;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserProfile FindUserByLoginName(string login)
        {
            UserProfile u = UserProfileDao.FindByLoginName(login);
            return u;
        }

        [Transactional]
        public UserGroup AddGroup(string name, string description, long userId)
        {
            UserProfile u = UserProfileDao.Find(userId);
            UserGroup g = new UserGroup();
            g.name = name;
            g.description = description;
            g.UserProfiles.Add(u);
            GroupDao.Create(g);
            return g;
        }

        [Transactional]
        public ICollection<Recommendation> AddRecommendation(long eventId, ICollection<long> groups, long userId, string description)
        {
            Event e = EventDao.Find(eventId);
            ICollection<Recommendation> recs = new List<Recommendation>();
            foreach (long groupId in groups)
            {
                UserGroup g = GroupDao.Find(groupId);
                UserProfile u = UserProfileDao.Find(userId);
                Recommendation r = new Recommendation();
                r.UserGroup = g;
                r.UserProfile = u;
                r.Event = e;
                r.reason = description;
                RecommendationDao.Create(r);
                recs.Add(r);
            }
            return recs;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public ICollection<Recommendation> FindGroupRecommendations(long groupId, long userId, int startIndex, int count)
        {

            UserProfile u = UserProfileDao.Find(userId);
            UserGroup g = GroupDao.Find(groupId);
            if (g.UserProfiles.Contains(u))
            {
                ICollection<Recommendation> recs = RecommendationDao.FindByGroupId(groupId, startIndex, count);
                return recs;
            }
            else throw new Exception();
            //FILTRAR RECOMENDACIONES POR EVENTOS
            //Y SOLO DEBEN APARECER AQUELLAS DE LOS GRUPOS A LOS Q PERTENECE UN USUARIO
        }

        #endregion

    }

}