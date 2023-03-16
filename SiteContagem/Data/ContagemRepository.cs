using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using WorkerContagem.Models;

namespace SiteContagem.Data;

public class ContagemRepository
{
    private readonly IConfiguration _configuration;

    public ContagemRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Save(ResultadoContador resultado)
    {
        using var conexao = new SqlConnection(
            _configuration.GetConnectionString("BaseContagem"));
        conexao.Insert<HistoricoContagem>(new()
        {
            DataProcessamento = DateTime.UtcNow.AddHours(-3), // Horário padrão do Brasil
            ValorAtual = resultado.ValorAtual,
            Producer = resultado.Producer,
            NodeK8s = resultado.NodeK8s,
            NamespaceK8s = resultado.NamespaceK8s,
            Mensagem = resultado.Mensagem,
            Kernel = resultado.Kernel,
            Framework = resultado.Framework
        });
    }
}