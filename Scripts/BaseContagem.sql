CREATE DATABASE BaseContagem
GO

USE BaseContagem
GO

CREATE TABLE dbo.HistoricoContagem(
    Id INT IDENTITY(1,1) NOT NULL,
    DataProcessamento DATETIME NOT NULL,
    ValorAtual INT NOT NULL,
    Producer VARCHAR(120) NOT NULL,
    NamespaceK8s VARCHAR(120) NOT NULL,
    NodeK8s VARCHAR(500) NOT NULL,
    Kernel VARCHAR(80) NOT NULL,
    Mensagem VARCHAR(500) NOT NULL,
    Framework VARCHAR(80) NOT NULL,
    FlProcessado BIT DEFAULT 0 NOT NULL,
    CONSTRAINT PK_HistoricoContagem PRIMARY KEY (Id)
)
GO