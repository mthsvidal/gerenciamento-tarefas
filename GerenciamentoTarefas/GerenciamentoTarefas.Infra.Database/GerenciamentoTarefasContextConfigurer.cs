using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoTarefas.Infra.Database
{
    public static class GerenciamentoTarefasContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GerenciamentoTarefasContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GerenciamentoTarefasContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
