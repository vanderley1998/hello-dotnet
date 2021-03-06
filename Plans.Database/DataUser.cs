﻿using Plans.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace Plans.Database
{
    public class DataUser : ICrud<User>
    {
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> list = PlanModuleDB.ConnectionDB.Query<User>(@"
                SELECT
                    ID, NAME,
                    REGISTER_DATE AS RegisterDate,
                    LAST_CHANGED_DATE AS LastchangedDate,
                    CAN_CREATE_PLAN AS CanCreatePlan,
                    REMOVED
                FROM USERS
            ");
            return list;
        }

        public bool Delete(int id)
        {
            int affectedLines = PlanModuleDB.ConnectionDB.Execute($"UPDATE USERS SET REMOVED = 1 WHERE ID = @Id", new { Id = id });
            return affectedLines > 0;
        }

        public User Save(User obj)
        {
            string query;
            if (obj.Id == 0)
            {
                query = @"
                    INSERT INTO USERS (NAME, REGISTER_DATE, LAST_CHANGED_DATE, CAN_CREATE_PLAN, REMOVED)
                    VALUES (@Name, @RegisterDate, @LastchangedDate, @CanCreatePlan, @Removed);
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";
                var planStatusInserted = PlanModuleDB.ConnectionDB
                    .Query<int>(query, param: new { obj.Name, obj.RegisterDate, obj.LastchangedDate, obj.CanCreatePlan, obj.Removed });
                obj.Id = planStatusInserted.Single();
                return obj;
            }
            else
            {
                query = @"UPDATE USERS SET NAME = @Name, CAN_CREATE_PLAN = @CanCreatePlan, REMOVED = @Removed WHERE ID = @Id";
                int affectedLines = PlanModuleDB.ConnectionDB
                    .Execute(query, param: new { obj.Id, obj.Name, obj.CanCreatePlan, obj.Removed });
                return affectedLines > 0 ? obj : throw new ArgumentException($"There's no User with id = {obj.Id} in database.");
            }
        }

        public User Get(int id)
        {
            try
            {
                var userFound = PlanModuleDB.ConnectionDB
                    .Query<User>(@"
                        SELECT
                            ID, NAME,
                            REGISTER_DATE AS RegisterDate,
                            LAST_CHANGED_DATE AS LastchangedDate,
                            CAN_CREATE_PLAN AS CanCreatePlan,
                            REMOVED
                        FROM USERS
                        WHERE ID = @id
                    ", param: new { id });
                return userFound.First();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public IEnumerable<User> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
