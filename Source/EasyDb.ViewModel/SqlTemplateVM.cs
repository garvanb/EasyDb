using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyDb.ViewModel
{
    public class SqlTemplateVM : RulesTemplateVM
    {
        private bool _addAnsiNull;
        public bool AddAnsiNull
        {
            get { return _addAnsiNull; }
            set { SetValue(ref _addAnsiNull, value, "AddAnsiNull"); }
        }
        private bool _addQuotedIdentifier;
        public bool AddQuotedIdentifier
        {
            get { return _addQuotedIdentifier; }
            set { SetValue(ref _addQuotedIdentifier, value, "AddQuotedIdentifier"); }
        }
        private bool _includeBeginEnd;
        public bool IncludeBeginEnd
        {
            get { return _includeBeginEnd; }
            set { SetValue(ref _includeBeginEnd, value, "IncludeBeginEnd"); }
        }
        private bool _inlcudeIfExists;
        public bool IncludeIfExists
        {
            get { return _inlcudeIfExists; }
            set { SetValue(ref _inlcudeIfExists, value, "IncludeIfExistsDrop"); }
        }
        private bool _includeDrop;
        public bool IncludeDrop
        {
            get { return _includeDrop; }
            set { SetValue(ref _includeDrop, value, "IncludeDrop"); }
        }
        private string _prefixNameWith;
        public string PrefixNameWith
        {
            get { return _prefixNameWith; }
            set { SetValue(ref _prefixNameWith, value, "PrefixNameWith"); }
        }
        private string _prefixNameForBulkSaveWith;
        public string PrefixNameForBulkSaveWith
        {
            get { return _prefixNameForBulkSaveWith; }
            set { SetValue(ref _prefixNameForBulkSaveWith, value, "PrefixNameForBulkSaveWith"); }
        }
        private SqlStatementType _sqlStatementType;
        public SqlStatementType SqlStatementType
        {
            get { return _sqlStatementType; }
            set { SetValue(ref _sqlStatementType, value, "SqlStatementType"); }
        }
        private SqlObjectType _sqlObjectType;
        public SqlObjectType SqlObjectType
        {
            get { return _sqlObjectType; }
            set { SetValue(ref _sqlObjectType, value, "SqlObjectType"); }
        }
        private bool _useParametersToGenerateData;
        public bool UseParametersToGenerateData
        {
            get { return _useParametersToGenerateData; }
            set { SetValue(ref _useParametersToGenerateData, value, "UseParametersToGenerateData"); }
        }
        private bool _enableUseParameters;
        public bool EnableUseParameters
        {
            get { return _enableUseParameters; }
            set { SetValue(ref _enableUseParameters, value, "EnableUseParameters"); }
        }
        private int TemplateId;

        public ICommand StatementTypeChangedCommand
        { get { return new RelayCommand(parameter => StatementTypeChanged()); } }

        public SqlTemplateVM(RulesTemplate rulesTemplate)
        {
            SqlTemplate rules = (SqlTemplate)rulesTemplate;
            Id = rulesTemplate.Id;
            TemplateId = rules.TemplateId;
            AddAnsiNull = rules.AddAnsiNull;
            AddQuotedIdentifier = rules.AddQuotedIdentifier;
            IncludeBeginEnd = rules.IncludeBeginEnd;
            IncludeIfExists = rules.IncludeIfExistsDrop;
            IncludeDrop = rules.IncludeDrop;
            PrefixNameWith = rules.PrefixNameWith;
            PrefixNameForBulkSaveWith = rules.PrefixNameForBulkSaveWith;
            SqlStatementType = rules.StatementType;
            SqlObjectType = rules.ObjectType;
            UseParametersToGenerateData = rules.UseParametersToGenerateData;
        }

        public SqlTemplate ToSqlTemplate()
        {
            SqlTemplate sqlTemplate = new SqlTemplate();
            sqlTemplate.Id = Id;
            sqlTemplate.AddAnsiNull = AddAnsiNull;
            sqlTemplate.AddQuotedIdentifier = AddQuotedIdentifier;
            sqlTemplate.IncludeBeginEnd = IncludeBeginEnd; 
            sqlTemplate.IncludeIfExistsDrop = IncludeIfExists;
            sqlTemplate.IncludeDrop = IncludeDrop;
            sqlTemplate.PrefixNameWith = PrefixNameWith;
            sqlTemplate.PrefixNameForBulkSaveWith = PrefixNameForBulkSaveWith;
            sqlTemplate.StatementType = SqlStatementType;
            sqlTemplate.ObjectType = SqlObjectType;
            sqlTemplate.UseParametersToGenerateData = UseParametersToGenerateData;
            return sqlTemplate;
        }

        private void StatementTypeChanged()
        {
            if (SqlStatementType == SqlStatementType.Insert)
            {
                EnableUseParameters = true;
            }
            else
            {
                UseParametersToGenerateData = false;
                EnableUseParameters = false;
            }
        }
    }
}
