﻿using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface ICreditRepository
    {
        public Credit CreateCredit();

        /// <summary>
        /// Lấy danh sách Credit tìm kiếm của tất cả user, sắp xếp theo credit có nhiều ng học nhất
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<CreditDTO> GetListCreditByFilter(SearchBase searchBase);

        /// <summary>
        /// Lấy danh sách Credit của một user đang đăng nhập
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<CreditDTO> GetListCreditByUser(SearchBase searchBase, string username = "");

        public Credit GetCreditById(string creditId);
    }
}
