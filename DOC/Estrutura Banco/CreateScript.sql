CREATE TABLE Pessoa (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(30)   NOT NULL ,
  Sobrenome VARCHAR(50)   NOT NULL ,
  Genero VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE TipoRelacao (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(50) UNIQUE NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE TipoDeExecucao (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(40) UNIQUE NOT NULL   ,
  Binario BOOL   NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE LocalizacaoGeografica (
  Codigo SERIAL  NOT NULL ,
  Latitude DOUBLE PRECISION   NOT NULL ,
  Longitude DOUBLE PRECISION   NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE Apelido (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(Codigo));

CREATE TABLE Idioma (
  Codigo SERIAL  NOT NULL ,
  Nome VARCHAR(20)  UNIQUE NOT NULL ,
PRIMARY KEY(Codigo));

CREATE TABLE Tipo (
  Codigo SERIAL  NOT NULL ,
  TipoDeExecucao INTEGER   NOT NULL ,
  Nome VARCHAR(30)  UNIQUE NOT NULL ,
  Extensao VARCHAR(7)   NOT NULL   ,
PRIMARY KEY(Codigo)  ,
  FOREIGN KEY(TipoDeExecucao)
    REFERENCES TipoDeExecucao(Codigo));

CREATE INDEX Tipo_FKIndex1 ON Tipo (TipoDeExecucao);

CREATE INDEX IFK_Rel_27 ON Tipo (TipoDeExecucao);

CREATE TABLE PessoaApelido (
  Codigo SERIAL  NOT NULL ,
  Apelido INTEGER   NOT NULL ,
  Pessoa INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Pessoa)
    REFERENCES Pessoa(Codigo),
  FOREIGN KEY(Apelido)
    REFERENCES Apelido(Codigo));

CREATE INDEX PessoaApelido_FKIndex1 ON PessoaApelido (Pessoa);
CREATE INDEX PessoaApelido_FKIndex2 ON PessoaApelido (Apelido);

CREATE INDEX IFK_Rel_07 ON PessoaApelido (Pessoa);
CREATE INDEX IFK_Rel_08 ON PessoaApelido (Apelido);

CREATE TABLE Registro (
  Codigo SERIAL  NOT NULL ,
  Idioma INTEGER   NOT NULL ,
  Tipo INTEGER   NOT NULL ,
  Nome VARCHAR(30) UNIQUE  NOT NULL ,
  Conteudo TEXT   NOT NULL ,
  DataInsercao TIMESTAMP   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Tipo)
    REFERENCES Tipo(Codigo),
  FOREIGN KEY(Idioma)
    REFERENCES Idioma(Codigo));

CREATE INDEX Registro_FKIndex1 ON Registro (Tipo);
CREATE INDEX Registro_FKIndex2 ON Registro (Idioma);

CREATE INDEX IFK_Rel_17 ON Registro (Tipo);
CREATE INDEX IFK_Rel_23 ON Registro (Idioma);

CREATE TABLE PessoaRegistro (
  Codigo SERIAL  NOT NULL ,
  TipoRelacao INTEGER   NOT NULL ,
  Registro INTEGER   NOT NULL ,
  Pessoa INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)      ,
  FOREIGN KEY(Pessoa)
    REFERENCES Pessoa(Codigo),
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo),
  FOREIGN KEY(TipoRelacao)
    REFERENCES TipoRelacao(Codigo));

CREATE INDEX PessoaDocumento_FKIndex1 ON PessoaRegistro (Pessoa);
CREATE INDEX PessoaDocumento_FKIndex2 ON PessoaRegistro (Registro);
CREATE INDEX PessoaDocumento_FKIndex3 ON PessoaRegistro (TipoRelacao);

CREATE INDEX IFK_Rel_09 ON PessoaRegistro (Pessoa);
CREATE INDEX IFK_Rel_10 ON PessoaRegistro (Registro);
CREATE INDEX IFK_Rel_11 ON PessoaRegistro (TipoRelacao);

CREATE TABLE Descricao (
  Codigo SERIAL  NOT NULL ,
  Registro INTEGER   NOT NULL ,
  Conteudo TEXT   NOT NULL   ,
PRIMARY KEY(Codigo)  ,
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo));

CREATE INDEX Descricao_FKIndex1 ON Descricao (Registro);

CREATE INDEX IFK_Rel_14 ON Descricao (Registro);

CREATE TABLE RegistroLocalizacao (
  Codigo SERIAL  NOT NULL ,
  Registro INTEGER   NOT NULL ,
  LocalizacaoGeografica INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(LocalizacaoGeografica)
    REFERENCES LocalizacaoGeografica(Codigo),
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo));

CREATE INDEX RegistroLocalizacao_FKIndex1 ON RegistroLocalizacao (LocalizacaoGeografica);
CREATE INDEX RegistroLocalizacao_FKIndex2 ON RegistroLocalizacao (Registro);

CREATE INDEX IFK_Rel_21 ON RegistroLocalizacao (LocalizacaoGeografica);
CREATE INDEX IFK_Rel_22 ON RegistroLocalizacao (Registro);

CREATE TABLE RegistroApelido (
  Codigo SERIAL  NOT NULL ,
  Registro INTEGER   NOT NULL ,
  Apelido INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Apelido)
    REFERENCES Apelido(Codigo),
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo));

CREATE INDEX RegistroApelido_FKIndex1 ON RegistroApelido (Apelido);
CREATE INDEX RegistroApelido_FKIndex2 ON RegistroApelido (Registro);

CREATE INDEX IFK_Rel_12 ON RegistroApelido (Apelido);
CREATE INDEX IFK_Rel_13 ON RegistroApelido (Registro);

CREATE TABLE Referencia (
  Codigo SERIAL  NOT NULL ,
  Referencia INTEGER   NOT NULL ,
  Registro INTEGER   NOT NULL   ,
PRIMARY KEY(Codigo)    ,
  FOREIGN KEY(Registro)
    REFERENCES Registro(Codigo),
  FOREIGN KEY(Referencia)
    REFERENCES Registro(Codigo));

CREATE INDEX RegistroRelacionado_FKIndex1 ON Referencia (Registro);
CREATE INDEX RegistroRelacionado_FKIndex2 ON Referencia (Referencia);

CREATE INDEX IFK_Rel_24 ON Referencia (Registro);
CREATE INDEX IFK_Rel_25 ON Referencia (Referencia);