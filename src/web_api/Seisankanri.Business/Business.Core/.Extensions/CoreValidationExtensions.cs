using Business.Core.Extensions;
using Business.Core.Interfaces;
using FluentValidation;
using Framework.Core.Abstractions;
using Framework.Core.Extensions;

namespace Seisankanri.Business.Core.Extensions
{
    public static class CoreValidationExtensions
    {
        public static IRuleBuilderOptionsConditions<T, R> NotNullOrEmpty<T, R>(this IRuleBuilder<T, R> ruleBuilder)
        {
            return ruleBuilder.Custom((x, y) =>
            {
                if (x == null || string.IsNullOrEmpty(x.ToString()))
                    // TODO: Translate message
                    y.AddFailure(y.DisplayName, $"{y.DisplayName} is required");
            });
        }

        public static IRuleBuilderOptionsConditions<T, R> NotNullOrEmpty<T, R>(this IRuleBuilder<T, R> ruleBuilder, ILocalizeServices ls)
        {
            return ruleBuilder.Custom((x, y) =>
            {
                if (x == null || string.IsNullOrEmpty(x.ToString()))
                    y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_009));
            });
        }

        public static IRuleBuilderOptionsConditions<T, R> MaxLength<T, R>(this IRuleBuilder<T, R> ruleBuilder, ILocalizeServices ls, int maxLength)
        {
            return ruleBuilder.Custom((x, y) =>
            {
            if (x != null &&  !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length > maxLength)
                    y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_010));
            });
        }
        public static IRuleBuilderOptionsConditions<T, R> MinLength<T, R>(this IRuleBuilder<T, R> ruleBuilder, ILocalizeServices ls, int minLength)
        {
            return ruleBuilder.Custom((x, y) =>
            {
                if (x != null && !string.IsNullOrEmpty(x.ToString()) && x.ToString().Length < minLength)
                    y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_010));
            });
        }

        public static IRuleBuilderOptionsConditions<T, R> IsValidDate<T, R>(this IRuleBuilder<T, R> ruleBuilder, ILocalizeServices ls)
        {
            return ruleBuilder.Custom((x, y) =>
            {
                if (x != null)
                {
                    var dateParse = x.ToString().ToDate();
                    if (dateParse == null)
                        y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_008));
                }
            });
        }

        public static IRuleBuilderOptionsConditions<T, R> IsValidDateTime<T, R>(this IRuleBuilder<T, R> ruleBuilder, ILocalizeServices ls)
        {
            return ruleBuilder.Custom((x, y) =>
            {
                if (x != null)
                {
                    var datetimeParse = x.ToString().ToDateTime();
                    if (datetimeParse == null)
                        y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_008));
                }
                else
                    y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_008));
            });
        }
        
        public static IRuleBuilderOptionsConditions<T, R> MasterMustExist<T, R, TEntity>(this IRuleBuilder<T, R> ruleBuilder, IRepository<TEntity> repo, ILocalizeServices ls, string field_name) where TEntity : class, IAudit, ISoftDelete, IEntity<Guid>
        {
            var data = repo.GetQuery().ExcludeSoftDeleted().ToArray();
            return ruleBuilder.Custom((x, y) =>
            {
                if (x != null)
                {
                    var result =  data.Select(m => m.GetType().GetProperty(field_name).GetValue(m, null).ToString())
                                      .Where(z => x.Equals(z)).FirstOrDefault();
                    if (result == null)
                        y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_005));
                }
            });
        }
        
        public static IRuleBuilderOptionsConditions<T, R> MasterMustNotExist<T, R, TEntity>(this IRuleBuilder<T, R> ruleBuilder, IRepository<TEntity> repo, ILocalizeServices ls, string field_name) where TEntity : class, IAudit, ISoftDelete, IEntity<Guid>
        {
            var data = repo.GetQuery().ExcludeSoftDeleted().ToArray();
            return ruleBuilder.Custom((x, y) =>
            {
                string data_feild;
                data_feild = x == null ? string.Empty : x.ToString();

                var result = data.Select(m => m.GetType().GetProperty(field_name).GetValue(m, null).ToString())
                                  .Where(y => data_feild.Equals(y)).FirstOrDefault();
                if (result == null)
                    y.AddFailure(y.DisplayName, ls.Get(Modules.CORE, "Message", MessageKey.E_005));
            });
        }
    }
}
