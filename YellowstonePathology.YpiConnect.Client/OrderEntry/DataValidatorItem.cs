using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class DataValidatorItem
    {
        private YellowstonePathology.Shared.ValidationResult m_DataTypeValidationResult;        
        private BindingExpression m_BindingExpression;
        private Func<YellowstonePathology.Shared.ValidationResult> m_DomainValidationMethod;
        private YellowstonePathology.Shared.ValidationResult m_DomainValidationResult;

		public DataValidatorItem(YellowstonePathology.Shared.ValidationResult dataTypeValidationResult, BindingExpression bindingExpression, Func<YellowstonePathology.Shared.ValidationResult> domainValidationMethod)
        {
            this.m_DataTypeValidationResult = dataTypeValidationResult;            
            this.m_BindingExpression = bindingExpression;
            this.m_DomainValidationMethod = domainValidationMethod;
        }

        public YellowstonePathology.Shared.ValidationResult DataTypeValidationResult
        {
            get { return this.m_DataTypeValidationResult; }
            set {this.m_DataTypeValidationResult = value;}
        }        

        public BindingExpression BindingExpression
        {
            get { return this.m_BindingExpression; }
            set { this.m_BindingExpression = value; }
        }

        public Func<YellowstonePathology.Shared.ValidationResult> DomainValidationMethod
        {
            get { return this.m_DomainValidationMethod; }
            set { this.m_DomainValidationMethod = value; }
        }

        public YellowstonePathology.Shared.ValidationResult DomainValidationResult
        {
            get { return this.m_DomainValidationResult; }
            set { this.m_DomainValidationResult = value; }
        }        
    }
}
