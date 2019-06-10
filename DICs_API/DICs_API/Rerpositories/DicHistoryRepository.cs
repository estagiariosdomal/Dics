﻿using Dapper;
using DICs_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Rerpositories
{
    public class DicHistoryRepository : AbstractRepository<DicHistory>
    {
        public DicHistoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override bool Delete(int id)
        {
            throw new Exception("Não é permitida a exclusão de nenhum histórico");
        }

        public override DicHistory Get(int id)
        {
            throw new Exception("Utilize o método GetAll pra obter os históricos passando como parametro o id");
        }

        public override IEnumerable<DicHistory> GetAll()
        {
            throw new Exception("Utilize o método GetAll pra obter os históricos passando como parametro o id");
        }
        
        public IEnumerable<DicHistory>GetAll(int id)
        {
            using( IDbConnection db = new SqlConnection(ConnectionString))
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var query = db.Query<DicHistory, Status, DicHistory>(@"SELECT h.*, s.*
                                FROM DIC_HISTORY h INNER JOIN DIC d ON h.ID_DIC = d.ID
                                INNER JOIN STATUS s ON h.ID_STATUS_DIC = s.ID
                                WHERE d.ID = @Id"
                            , (h, s) =>
                             {
                                 h.StatusDic = s;
                                 return h;
                             }, new { Id = id }, splitOn:"id,id");
                return query;
            }

        }

        public override DicHistory GetLastInserted()
        {
            throw new Exception();
        }

        //implementar
        public override bool Insert(DicHistory item)
        {
            throw new NotImplementedException();
        }

        //não implementar
        public override bool Update(DicHistory item)
        {
            throw new NotImplementedException();
        }
    }
}
