using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IFlashcardRepository
    {
        /// <summary>
        /// Lấy danh sách Flashcard của 1 bộ thẻ và tình trạng đang học của user đó
        /// </summary>
        /// <param name="creditId">Mã bộ thẻ</param>
        /// <param name="username">username truy vấn thẻ</param>
        /// <returns></returns>
        public List<Flashcard> GetFlashcardByCreditId(string creditId, string username);
    }
}
