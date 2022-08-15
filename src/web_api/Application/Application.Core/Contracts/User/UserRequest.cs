using FluentValidation;
using Application.Common.Abstractions;
using Domain.Entities;
using Application.Common.Extensions;

namespace Application.Core.Contracts 
{
    public class UserRequest
    {
        public string code { get; set; }
        public string full_name { get; set; }
        public string user_name { get; set; }
        public string mail { get; set; }
        public string? phone { get; set; }
        public bool? is_actived { get; set; }
        public string? role_cd { get; set; }
        public string? furigana { get; set; }
        public string? branch_cd { get; set; }
        public string? deparment_cd { get; set; }
        public string? jobtitle_cd { get; set; }

    }

    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator(IUnitOfWork _unitOfWork, ILocalizeServices _ls)
        {
            var repo = _unitOfWork.GetRepository<Role>();
            var repoCompany = _unitOfWork.GetRepository<Company>();
            var repoClassification = _unitOfWork.GetRepository<Classification>();

            RuleFor(_ => _.code).NotNullOrEmpty().MaximumLength(10).MasterMustNotExist(repo, _ls, "code");
            RuleFor(_ => _.full_name).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.user_name).NotNullOrEmpty().MaximumLength(50);
            RuleFor(_ => _.mail).NotNullOrEmpty().EmailAddress().MaximumLength(50);
            RuleFor(_ => _.phone).NotNullOrEmpty().MaximumLength(15);
            RuleFor(_ => _.role_cd).NotNullOrEmpty().MaximumLength(10).MasterMustExist(repo, _ls, "code");
            RuleFor(_ => _.furigana).NotNullOrEmpty().MaximumLength(100);
            RuleFor(_ => _.branch_cd).NotNullOrEmpty().MasterMustExist(repoCompany, _ls, "code");
            RuleFor(_ => _.deparment_cd).NotNullOrEmpty().MasterMustExist(repoClassification, _ls, "code2");
            RuleFor(_ => _.jobtitle_cd).NotNullOrEmpty().MasterMustExist(repoClassification, _ls, "code2");
        }
    }
}
