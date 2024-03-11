using AutoMapper;
using GP.Common.DTO;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Helpers
{
    public class MappingProfile
    {
        private readonly IMapper _mapper;
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // example
                cfg.CreateMap<Flashcard, FlashcardDTO>().ReverseMap();
                cfg.CreateMap<Account, AccountDTO>(); //.ForMember(account => account.CreatedAt, act => act.MapFrom(dto => dto.CreatedAt)).ReverseMap();
                cfg.CreateMap<Notification, NotificationDTO>().ReverseMap();
                cfg.CreateMap<Credit, CreditDTO>().ReverseMap();
                cfg.CreateMap<Learn, LearnDTO>().ReverseMap();
                cfg.CreateMap<Folder, FolderDTO>().ReverseMap();
                cfg.CreateMap<Class, ClassDTO>().ReverseMap();
                //.ForMember(folder => folder.Flashcard, act => act.MapFrom(dto => dto.Flashcard)).ReverseMap();
            });

            _mapper = config.CreateMapper();

        }

        // example
        public FlashcardDTO MapFlashcardToDTO(Flashcard flashcard)
        {
            return _mapper.Map<FlashcardDTO>(flashcard);
        }
        public Flashcard MapDTOToFlashcard(FlashcardDTO flashcardDTO)
        {
            return _mapper.Map<Flashcard>(flashcardDTO);
        }
        public AccountDTO MapAccountToDTO(Account account)
        {
            return _mapper.Map<AccountDTO>(account);
        }
        public Account MapDTOToAccount(AccountDTO accountDTO)
        {
            return _mapper.Map<Account>(accountDTO);
        }
        public NotificationDTO MapNotiToDTO(Notification notification)
        {
            return _mapper.Map<NotificationDTO>(notification);
        }
        public Notification MapDTOToNoti(NotificationDTO notificationDTO)
        {
            return _mapper.Map<Notification>(notificationDTO);
        }
        public CreditDTO MapCreditToDTO(Credit credit)
        {
            return _mapper.Map<CreditDTO>(credit);
        }
        public Credit MapDTOToCredit(CreditDTO creditDTO)
        {
            return _mapper.Map<Credit>(creditDTO);
        }

        public LearnDTO MapLearnToDTO(Learn learn)
        {
            return _mapper.Map<LearnDTO>(learn);
        }
        public Learn MapDTOToLearn(LearnDTO learnDTO)
        {
            return _mapper.Map<Learn>(learnDTO);
        }

        public FolderDTO MapFolderToDTO(Folder folder)
        {
            return _mapper.Map<FolderDTO>(folder);
        }
        public Folder MapDTOToFolder(FolderDTO folderDTO)
        {
            return _mapper.Map<Folder>(folderDTO);
        }
        public ClassDTO MapClassToDTO(Class _class)
        {
            return _mapper.Map<ClassDTO>(_class);
        }
        public Class MapDTOToClass(ClassDTO classDTO)
        {
            return _mapper.Map<Class>(classDTO);
        }

        // Map List

        public List<FlashcardDTO> MapFlashcardsToDTOs(List<Flashcard> flashcards)
        {
            return _mapper.Map<List<FlashcardDTO>>(flashcards);
        }

        public List<LearnDTO> MapLearnsToDTOs(List<Learn> learns)
        {
            return _mapper.Map<List<LearnDTO>>(learns);
        }
        public List<Learn> MapDTOsToLearns(List<LearnDTO> learnDTOs)
        {
            return _mapper.Map<List<Learn>>(learnDTOs);
        }

        public List<AccountDTO> MapAccountsToDTOs(List<Account> accounts)
        {
            return _mapper.Map<List<AccountDTO>>(accounts);
        }
        public List<Account> MapDTOsToAccounts(List<AccountDTO> accountDTOs)
        {
            return _mapper.Map<List<Account>>(accountDTOs);
        }
    }
}
