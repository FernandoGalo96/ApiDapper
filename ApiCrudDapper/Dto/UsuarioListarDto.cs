﻿namespace ApiCrudDapper.Dto;

public class UsuarioListarDto
{
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Cargo { get; set; }

    public double Salario { get; set; }

    public bool Situacao { get; set; }
}