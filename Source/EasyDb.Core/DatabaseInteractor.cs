using EasyDb.Data;
using EasyDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.Data
{
    public class DatabaseInteractor
    {
        private string _connectionString;

        public DatabaseInteractor()
        {
            if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["EasyDb"].ConnectionString;
            }
        }

        /// <summary>
        /// Used when changing server to populate databases
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="ignoreDatabase">Used to distinguish signature from other constructor</param>
        public DatabaseInteractor(string serverName, bool ignoreDatabase)
        {
            _connectionString = string.Format(@"Server={0};Trusted_Connection=True;", serverName);
        }

        public DatabaseInteractor(string dbName)
        {
            _connectionString = string.Format(@"Server={0};Trusted_Connection=True;database={1}", Properties.Settings.Default.DefaultServer, dbName);
        }

        private void ExecuteCommand(SqlCommand command)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                command.Connection = conn;
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<string> GetDatabases()
        {
            ObservableCollection<string> databases = new ObservableCollection<string>();
            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader["Name"].ToString());
                    }
                }
            }
            return databases;
        }
      
        /// <summary>
        /// Gets all tables for the specified database
        /// </summary>
        /// <returns></returns>
        public DataTable GetTables()
        {
            DataTable tables = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                tables = conn.GetSchema("Tables");
            }
            return tables;
        }

        /// <summary>
        /// Gets all columns for a table
        /// </summary>
        /// <returns></returns>
        public DataTable GetColumns(string tablename)
        {
            DataTable columns = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string[] restrictions = new string[4] { null, null, tablename, null };
                conn.Open();
                columns = conn.GetSchema("Columns", restrictions);
            }
            return columns;
        }

        /// <summary>
        /// Gets all the data in a table
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableData(string tablename, string columnClause)
        {
        
            DataTable data = new DataTable();
            string query = string.Format("SELECT {0} from {1}", columnClause, tablename);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(data);
            }
            return data;
        }

        /// <summary>
        /// Gets all templates
        /// </summary>
        /// <returns></returns>
        public List<Template> GetTemplates()
        {
            List<Template> templates = new List<Template>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GetTemplates", conn) { CommandType = CommandType.StoredProcedure };

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Template template = new Template();
                        template.Id = (int)reader["Id"];
                        template.Name = (string)reader["Name"];
                        template.IsStandard = (bool)reader["IsStandardTemplate"];
                        TemplateType type = (TemplateType)reader["LanguageId"];
                        template.Type = (LanguageType)reader["LanguageId"];
                        if (template.Type == LanguageType.SQL)
                        {
                            SqlTemplate sqlTemplate = new SqlTemplate();
                            if (reader["SqlTemplateId"] != DBNull.Value)
                            {
                                sqlTemplate.Id = (int)reader["SqlTemplateId"];
                                sqlTemplate.AddAnsiNull = (bool)reader["AddAnsiNulls"];
                                sqlTemplate.AddQuotedIdentifier = (bool)reader["AddQuotedIdentifier"];
                                sqlTemplate.IncludeBeginEnd = (bool)reader["IncludeBeginEnd"];
                                sqlTemplate.IncludeIfExistsDrop = (bool)reader["IncludeIfExistsDrop"];
                                sqlTemplate.IncludeDrop = (bool)reader["IncludeDrop"];
                                sqlTemplate.PrefixNameWith = (reader["PrefixNameWith"] == DBNull.Value) ? "" : reader["PrefixNameWith"].ToString();
                                sqlTemplate.PrefixNameForBulkSaveWith = (reader["PrefixNameWith"] == DBNull.Value) ? "" : reader["PrefixNameForBulkSaveWith"].ToString();
                                sqlTemplate.ObjectType = (SqlObjectType)(reader["ObjectTypeId"]);
                                sqlTemplate.StatementType = (reader["StatementTypeId"] == DBNull.Value) ? 0 : (SqlStatementType)reader["StatementTypeId"];
                                sqlTemplate.UseParametersToGenerateData = (reader["UseParametersToGenerateData"] == DBNull.Value) ? false : (bool)reader["UseParametersToGenerateData"];
                                template.RulesTemplate = sqlTemplate;
                            }
                        }
                        else if (template.Type == LanguageType.CSharp)
                        {
                            CSharpTemplate cSharpTemplate = new CSharpTemplate();
                            cSharpTemplate.Id = reader.GetInt("CSharpTemplateId");
                            cSharpTemplate.ClassNameSuffix = reader.GetString("ClassNameSuffix");
                            cSharpTemplate.Namespace = reader.GetString("Namespace");
                            cSharpTemplate.IncludeUsings = reader.GetBool("IncludeUsings");
                            cSharpTemplate.IncludeProperties = reader.GetBool("IncludeProperties");
                            cSharpTemplate.IncludeEmptyConstructor = reader.GetBool("IncludeEmptyConstructor");
                            cSharpTemplate.IncludeOtherConstructor = reader.GetBool("IncludeOtherConstructor");
                            template.RulesTemplate = cSharpTemplate;
                        }
                        templates.Add(template);
                    }
                }
            }
            return templates;
        }

        /// <summary>
        /// Updates a template
        /// </summary>
        /// <returns></returns>
        public void UpdateTemplate(Template template)
        {
            SqlCommand command = new SqlCommand("EditTemplates") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Id", template.Id);
            command.Parameters.AddWithValue("@IsStandardTemplate", template.IsStandard);
            command.Parameters.AddWithValue("@Name", template.Name);
            command.Parameters.AddWithValue("@LanguageId", (int)template.Type);
            ExecuteCommand(command);
        }

        /// <summary>
        /// Updates a SqlTemplate
        /// </summary>
        /// <returns></returns>
        public void UpdateSqlTemplate(SqlTemplate sqlTemplate)
        {
            SqlCommand command = new SqlCommand("EditSqlTemplate") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Id", sqlTemplate.Id);
            command.Parameters.AddWithValue("@AddAnsiNulls", sqlTemplate.AddAnsiNull);
            command.Parameters.AddWithValue("@AddQuotedIdentifier", sqlTemplate.AddQuotedIdentifier);
            command.Parameters.AddWithValue("@IncludeBeginEnd", sqlTemplate.IncludeBeginEnd);
            command.Parameters.AddWithValue("@IncludeDrop", sqlTemplate.IncludeDrop);
            command.Parameters.AddWithValue("@IncludeIfExistsDrop", sqlTemplate.IncludeIfExistsDrop);
            command.Parameters.AddWithValue("@ObjectTypeId", (int)sqlTemplate.ObjectType);
            command.Parameters.AddWithValue("@PrefixNameForBulkSaveWith", sqlTemplate.PrefixNameForBulkSaveWith);
            command.Parameters.AddWithValue("@PrefixNameWith", sqlTemplate.PrefixNameWith);
            command.Parameters.AddWithValue("@StatementTypeId", (int)sqlTemplate.StatementType);
            command.Parameters.AddWithValue("@UseParametersToGenerateData", sqlTemplate.UseParametersToGenerateData);
            ExecuteCommand(command);
        }

        /// <summary>
        /// Updates a CSharpTemplate
        /// </summary>
        /// <returns></returns>
        public void UpdateCSharpTemplate(CSharpTemplate cSharpTemplate)
        {
            SqlCommand command = new SqlCommand("EditCSharpTemplate") { CommandType = CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@Id", cSharpTemplate.Id);
            command.Parameters.AddWithValue("@ClassNameSuffix", cSharpTemplate.ClassNameSuffix);
            command.Parameters.AddWithValue("@Namespace", cSharpTemplate.Namespace);
            command.Parameters.AddWithValue("@IncludeUsings", cSharpTemplate.IncludeUsings);
            command.Parameters.AddWithValue("@IncludeProperties", cSharpTemplate.IncludeProperties);
            command.Parameters.AddWithValue("@IncludeEmptyConstructor", cSharpTemplate.IncludeEmptyConstructor);
            command.Parameters.AddWithValue("@IncludeOtherConstructor", cSharpTemplate.IncludeOtherConstructor);
            ExecuteCommand(command);
        }

        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <returns></returns>
        public List<string> GetLanguages()
        {
            List<string> languages = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GetLanguages", conn) { CommandType = CommandType.StoredProcedure };

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        languages.Add(reader["Name"].ToString());
                    }
                }
            }
            return languages;
        }

        /// <summary>
        /// Gets all templates
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SqlColumnDescription> GetSqlColumnDescriptions()
        {
            ObservableCollection<SqlColumnDescription> columnDescriptions = new ObservableCollection<SqlColumnDescription>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM ColumnDescriptions", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SqlColumnDescription columnDescription = new SqlColumnDescription(
                            (int)reader["Value"], (string)reader["Description"], (string)reader["DataType"]);
                        columnDescriptions.Add(columnDescription);
                    }
                }
            }
            return columnDescriptions;
        }


        /// <summary>
        /// Gets all the identiy columns for a given table
        /// </summary>
        /// <returns></returns>
        public List<string> GetIdentityColumns(string tableName)
        {
            List<string> identityColumns = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string queryString = string.Format("SELECT name FROM sys.columns WHERE object_id = object_id('{0}') AND is_identity = 1", tableName);

                SqlCommand command = new SqlCommand(queryString, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        identityColumns.Add(reader["name"].ToString());
                    }
                }
            }
            return identityColumns;
        }
    }
}
