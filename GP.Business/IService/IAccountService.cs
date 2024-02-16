﻿using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IAccountService
    {
        #region Auth
        public void Register(AccountDTO accountDTO);

        public bool CheckUserExist(AccountDTO accountDTO, out string message);

        public bool VerifyLoginInfo(string username, string password, out string message);

        public AccountDTO CreateToken(string username);

        public void GenAndSetRefreshToken(HttpResponse response, string username = null);

        public Account GetCurrentAccount();

        public string GetCurrentUsername();

        public bool CheckValidRefreshToken(string refreshToken, out string message);

        #endregion

        #region read data
        /// <summary>
        /// Lấy danh sách Credit tìm kiếm của tất cả user, sắp xếp theo user nổi tiếng nhất 
        /// Điểm = sum(num_learn_credit) + sum(num_join_class)
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<AccountDTO> GetCreditByFilter(SearchBase searchBase);
        public PaginatedResultBase<Notification> GetNotiByUser(SearchBase searchBase);
        #endregion
    }
}
