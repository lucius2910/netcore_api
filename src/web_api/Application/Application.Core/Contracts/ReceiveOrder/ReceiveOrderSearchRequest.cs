using FluentValidation;
using Framework.Core.Extensions;
using Application.Common.Abstractions;
using Application.Common.Extensions;

namespace Application.Core.Contracts
{
    public class ReceiveOrderSearchRequest
    {
        public string order_date_start { get; set; } // 2020-02-02

        public string order_date_end { get; set; }

        public int? order_id_start { get; set; }

        public int? order_id_end { get; set; }

        public string company_cd { get; set; }

        public string customer_person_name { get; set; }

        public string record_on_page { get; set; }

        public int page { get; set; }

        public int size { get; set; }

        public string sort { get; set; }    

        public DateTime? order_date_start_date
        {
            get
            {
                return this.order_date_start.ToDate();
            }
        }
        public DateTime? order_date_end_date
        {
            get
            {
                return this.order_date_end.ToDate();
            }
        }
    }
    public class ReceiveOrderSearchRequestValidator : AbstractValidator<ReceiveOrderSearchRequest>
    {
        public ReceiveOrderSearchRequestValidator(ILocalizeServices ls)
        {
            RuleFor(_ => _.order_date_start).NotNullOrEmpty(ls).IsValidDate(ls);
        }
    }
}
